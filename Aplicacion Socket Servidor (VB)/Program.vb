Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Namespace AplicaciondeSocketVBNet
    Class Program
        Shared Sub Main(args As String())
            server()
        End Sub

        Public Shared Sub server()
            Dim host As IPHostEntry = Dns.GetHostEntry("localhost")
            Dim ipAddress As IPAddress = host.AddressList(0)
            Dim localEndPoint As New IPEndPoint(ipAddress, 11200)

            Try
                Dim listener As New Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
                listener.Bind(localEndPoint)
                listener.Listen(10)
                Console.WriteLine("Esperando conexión")

                Dim handler As Socket = listener.Accept()
                Dim data As String = Nothing
                Dim bytes As Byte() = Nothing
                While True
                    bytes = New Byte(1023) {}
                    Dim byteRec As Integer = handler.Receive(bytes)
                    data += Encoding.ASCII.GetString(bytes, 0, byteRec)
                    If data.IndexOf("<EOF>") > -1 Then
                        Exit While
                    End If
                End While
                Console.WriteLine("Texto del cliente:" + data)
            Catch e As Exception
                Console.WriteLine(e.ToString())
            End Try
        End Sub
    End Class
End Namespace
