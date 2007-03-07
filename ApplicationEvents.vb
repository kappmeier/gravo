Namespace My
  Partial Friend Class MyApplication
    Sub bla(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
      ' Startup: Wird beim Starten der Anwendung noch vor dem Erstellen des Startformulars ausgelöst.
    End Sub

    Sub bla2(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
      ' Shutdown: Wird nach dem Schließen aller Anwendungsformulare ausgelöst. Dieses Ereignis wird nicht ausgelöst, wenn die Anwendung nicht normal beendet wird.
    End Sub

    Sub bla3(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
      ' UnhandledException: Wird ausgelöst, wenn in der Anwendung eine unbehandelte Ausnahme auftritt.
      MsgBox("Eine unbehandelte Ausnahme ist aufgetreten. Das Programm wird nun beendet.", MsgBoxStyle.Critical, "Fehler")
    End Sub

    Sub bla4(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.Devices.NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
      ' NetworkAvailabilityChanged: Wird beim Herstellen oder Trennen der Netzwerkverbindung ausgelöst.
    End Sub

    Sub bla5(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
      ' StartupNextInstance: Wird beim Starten einer Einzelinstanzanwendung ausgelöst, wenn diese bereits aktiv ist. 
      MsgBox("Warunung! Sie führen mehrere Instanzen von " & Application.Info.ProductName & " aus. Dies kann zu inkonsistenzen in der Datenbank führen.", MsgBoxStyle.Information, "Warnung")
    End Sub
  End Class
End Namespace