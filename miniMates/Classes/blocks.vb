Public Class blocks
    Public Class blocks
        Public sources As List(Of sources)
        Public Function Check(s As sources)
            For Each itm As sources In sources
                If s.resource = itm.resource Then
                    If s.sprite = itm.sprite Then
                        Return True
                    End If
                End If
            Next
            Return False
        End Function
    End Class
    Public Class sources
        Public resource As String
        Public sprite As String
        Public type As String
        Public Sub New(r As String, s As String, t As String)
            resource = r
            sprite = s
            type = t
        End Sub

    End Class
End Class
