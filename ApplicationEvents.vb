Namespace My
  Partial Friend Class MyApplication
        Sub evt(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' Startup: Wird beim Starten der Anwendung noch vor dem Erstellen des Startformulars ausgelöst.
        End Sub

        Sub evt2(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            ' Shutdown: Wird nach dem Schließen aller Anwendungsformulare ausgelöst. Dieses Ereignis wird nicht ausgelöst, wenn die Anwendung nicht normal beendet wird.
        End Sub

        Sub evt3(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            ' UnhandledException: Wird ausgelöst, wenn in der Anwendung eine unbehandelte Ausnahme auftritt.
        End Sub

        Sub evt4(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.Devices.NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
            ' NetworkAvailabilityChanged: Wird beim Herstellen oder Trennen der Netzwerkverbindung ausgelöst.
        End Sub

        Sub evt5(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            ' StartupNextInstance: Wird beim Starten einer Einzelinstanzanwendung ausgelöst, wenn diese bereits aktiv ist. 
        End Sub
  End Class
End Namespace