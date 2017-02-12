'Version 1.0 {Can't Remember Date, forgot to put one in the first place}
Imports System.Runtime.InteropServices

Namespace ListResolutions

    Class DisplayResolutions
        <DllImport("user32.dll")> _
        Public Shared Function EnumDisplaySettings(deviceName As String, modeNum As Integer, ByRef devMode As DEVMODE) As Boolean
        End Function

        Const ENUM_CURRENT_SETTINGS As Integer = -1
        Const ENUM_REGISTRY_SETTINGS As Integer = -2

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure DEVMODE

            Private Const CCHDEVICENAME As Integer = &H20
            Private Const CCHFORMNAME As Integer = &H20
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=&H20)> _
            Public dmDeviceName As String
            Public dmSpecVersion As Short
            Public dmDriverVersion As Short
            Public dmSize As Short
            Public dmDriverExtra As Short
            Public dmFields As Integer
            Public dmPositionX As Integer
            Public dmPositionY As Integer
            Public dmDisplayOrientation As ScreenOrientation
            Public dmDisplayFixedOutput As Integer
            Public dmColor As Short
            Public dmDuplex As Short
            Public dmYResolution As Short
            Public dmTTOption As Short
            Public dmCollate As Short
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=&H20)> _
            Public dmFormName As String
            Public dmLogPixels As Short
            Public dmBitsPerPel As Integer
            Public dmPelsWidth As Integer
            Public dmPelsHeight As Integer
            Public dmDisplayFlags As Integer
            Public dmDisplayFrequency As Integer
            Public dmICMMethod As Integer
            Public dmICMIntent As Integer
            Public dmMediaType As Integer
            Public dmDitherType As Integer
            Public dmReserved1 As Integer
            Public dmReserved2 As Integer
            Public dmPanningWidth As Integer
            Public dmPanningHeight As Integer

        End Structure

        Shared Function GetAvailableResolutions(ByRef dmPelsWidth As List(Of Integer), ByRef dmPelsHeight As List(Of Integer)) As List(Of String)
            Dim AvailableResolutions As New List(Of String)

            Dim vDevMode As New DEVMODE()
            Dim i As Integer = 0
            While EnumDisplaySettings(Nothing, i, vDevMode)
                Dim CurReturnedResolution As String = String.Format("{0}x{1}", vDevMode.dmPelsWidth, vDevMode.dmPelsHeight)

                If Not isContained(CurReturnedResolution, AvailableResolutions, False) Then
                    AvailableResolutions.Add(CurReturnedResolution)
                    dmPelsWidth.Add(vDevMode.dmPelsWidth)
                    dmPelsHeight.Add(vDevMode.dmPelsHeight)
                End If

                i += 1

            End While

            Return AvailableResolutions
        End Function

    End Class

End Namespace