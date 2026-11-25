"""Tests for post_review.py (linkify, diff placement) and the review prompt."""

import sys
import unittest
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parent))

import post_review
import run_review

REPO = "kappmeier/gravo"
SHA = "abc123"


class LinkifyTextTest(unittest.TestCase):
    def test_bare_path_line_becomes_blob_link(self):
        out = post_review.linkify_text("see GravoCore/Deck.vb:42 for details", REPO, SHA)
        self.assertEqual(
            out,
            "see [GravoCore/Deck.vb:42]"
            "(https://github.com/kappmeier/gravo/blob/abc123/GravoCore/Deck.vb#L42) for details",
        )

    def test_backticked_path_line_loses_backticks_and_links(self):
        out = post_review.linkify_text("in `GravoCore/Deck.vb:42` the call is wrong", REPO, SHA)
        self.assertEqual(
            out,
            "in [GravoCore/Deck.vb:42]"
            "(https://github.com/kappmeier/gravo/blob/abc123/GravoCore/Deck.vb#L42) the call is wrong",
        )

    def test_backticked_path_without_line_links_without_fragment(self):
        out = post_review.linkify_text("see `post_review.py`", REPO, SHA)
        self.assertEqual(
            out,
            "see [post_review.py]"
            "(https://github.com/kappmeier/gravo/blob/abc123/post_review.py)",
        )

    def test_backticked_non_path_stays(self):
        text = "the `summary` key and `--pr` flag"
        self.assertEqual(post_review.linkify_text(text, REPO, SHA), text)

    def test_existing_urls_are_not_rewritten(self):
        text = (
            "see https://github.com/kappmeier/gravo/blob/abc123/GravoCore/Deck.vb#L42 "
            "and https://host:8080/x"
        )
        self.assertEqual(post_review.linkify_text(text, REPO, SHA), text)

    def test_path_with_space_is_quoted(self):
        out = post_review.linkify_text("my dir/F.vb:3", REPO, SHA)
        self.assertEqual(
            out,
            "[my dir/F.vb:3]"
            "(https://github.com/kappmeier/gravo/blob/abc123/my%20dir/F.vb#L3)",
        )

    def test_empty_repository_leaves_text_unchanged(self):
        self.assertEqual(post_review.linkify_text("GravoCore/Deck.vb:42", "", SHA), "GravoCore/Deck.vb:42")


SAMPLE_DIFF = """diff --git a/GravoCore/Deck.vb b/GravoCore/Deck.vb
--- a/GravoCore/Deck.vb
+++ b/GravoCore/Deck.vb
@@ -1,2 +1,3 @@
 context
+added line
-old line
@@ -10,1 +11,1 @@
+another
"""


class DiffPlacementTest(unittest.TestCase):
    def test_parse_diff_lines_collects_new_side_lines(self):
        lines = post_review.parse_diff_lines(SAMPLE_DIFF)
        self.assertEqual(
            lines,
            {("GravoCore/Deck.vb", 1), ("GravoCore/Deck.vb", 2), ("GravoCore/Deck.vb", 11)},
        )

    def test_split_findings_keeps_inline_comments_on_diff_lines(self):
        findings = [
            post_review.ReviewFinding("GravoCore/Deck.vb", 2, "on added line"),
            post_review.ReviewFinding("GravoCore/Deck.vb", 1, "on context line"),
            post_review.ReviewFinding("GravoCore/Deck.vb", 99, "off diff"),
        ]
        inline, off = post_review.split_findings(findings, post_review.parse_diff_lines(SAMPLE_DIFF))
        self.assertEqual([f.line for f in inline], [2, 1])
        self.assertEqual([f.line for f in off], [99])


class ReviewPromptTest(unittest.TestCase):
    def test_template_formats_and_asks_for_short_lines(self):
        template = run_review.load_prompt_template()
        rendered = template.format(readme="", solution="", layout="", pr_number="27", diff="")
        self.assertIn("80 to 100", rendered)


if __name__ == "__main__":
    unittest.main()
