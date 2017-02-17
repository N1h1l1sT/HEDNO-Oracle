'Version 1.3.2 2013-03-29
'Fixed some form size problems
Option Strict On

Imports System.IO
Imports System.Threading

Public Class frmPresentation
    Public strLanguage_Presentation() As String
    Public MeText As String
    Public CurFileNameAlone As String

    Dim strPresentationSubFolders() As String

    Dim strImages As List(Of String)
    Dim strVideos As List(Of String)

    Dim CurrentCountImages As Integer = 0
    Dim CurrentCountImagesStep As Integer = 0 'if there are only texts, then we divide by 1, if there's also photo, then by 2
    Dim CurrentCountVideos As Integer = 0
    Dim CurrentCountVideosStep As Integer = 0

    Dim ImagesAvailable As Boolean = False
    Dim VideosAvailable As Boolean = False

    Public WorkingOnImages As Boolean = True
    Public WorkingOnVideos As Boolean = True

    Dim isFormResizing As Boolean = False

    Private Sub frmPresentation_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

        Try
            Call Presentation_Language(Me)

            Call frmSkin(Me, False)
            If Directory.Exists(strPresentation) Then
                strPresentationSubFolders = Directory.GetDirectories(strPresentation)
                If strPresentationSubFolders.Length > 0 Then

                    strImages = Directory.GetFiles(strPresentation & "Images\").ToList
                    Dim ImgIndex As Integer = 0
                    For i = 0 To strImages.Count - 1
                        If strImages.Item(ImgIndex).EndsWith(".db") Then strImages.RemoveAt(ImgIndex) Else ImgIndex += 1
                    Next
                    If strImages.Count > 0 Then ImagesAvailable = True

                    strVideos = Directory.GetFiles(strPresentation & "Videos\").ToList
                    Dim VidIndex As Integer = 0
                    For i = 0 To strVideos.Count - 1
                        If strVideos.Item(VidIndex).EndsWith(".db") Then strVideos.RemoveAt(VidIndex) Else VidIndex += 1
                    Next
                    If strVideos.Count > 0 Then VideosAvailable = True

                    If ImagesAvailable OrElse VideosAvailable Then

                        Dim AlreadyLoadedImageExt As New List(Of String)
                        Dim AlreadyLoadedVideoExt As New List(Of String)

                        If WorkingOnImages = False AndAlso WorkingOnVideos = False Then WorkingOnImages = True

                        If (ImagesAvailable AndAlso WorkingOnImages) OrElse Not VideosAvailable Then
                            WorkingOnImages = True
                            Dim CurExt As String = GetExt(strImages(CurrentCountImages))
                            Dim isFinished As Boolean = False

                            Do Until isFinished OrElse isContained(CurExt, AlreadyLoadedImageExt)
                                CurFileNameAlone = GetFileNameAlone(strImages(CurrentCountImages))
                                AlreadyLoadedImageExt.Add(CurExt)

                                If strImages(CurrentCountImages).ToLower.EndsWith(".gif") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".jpg") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".jpeg") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".bmp") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".wmf") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".png") Then
                                    picInfo.BackgroundImageLayout = ImageLayout.None
                                    picInfo.BackgroundImage = Image.FromFile(strImages(CurrentCountImages))
                                    CurrentCountImages += 1
                                    CurrentCountImagesStep += 1
                                    picInfo.Visible = True
                                    wmpMedia.Visible = False
                                ElseIf strImages(CurrentCountImages).ToLower.EndsWith(".txt") Then
                                    ReadFile(strImages(CurrentCountImages), txtInfo)
                                    CurrentCountImages += 1
                                    CurrentCountImagesStep += 1
                                    txtInfo.Visible = True
                                    wbInfo.Visible = False
                                ElseIf strImages(CurrentCountImages).ToLower.EndsWith(".html") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".htm") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".xhtm") Then
                                    wbInfo.Navigate(strImages(CurrentCountImages))
                                    CurrentCountImages += 1
                                    CurrentCountImagesStep += 1
                                    txtInfo.Visible = False
                                    wbInfo.Visible = True
                                End If

                                If CurrentCountImages = strImages.Count Then
                                    isFinished = True
                                Else
                                    CurExt = GetExt(strImages(CurrentCountImages))
                                End If

                            Loop

                            'Just to count how many Videos are there
                            If VideosAvailable Then
                                Do Until isFinished OrElse isContained(CurExt, AlreadyLoadedVideoExt)
                                    AlreadyLoadedVideoExt.Add(CurExt)

                                    Dim Fil As String = strVideos(CurrentCountVideos).ToLower
                                    If Fil.EndsWith(".avi") OrElse Fil.EndsWith(".mp4") OrElse Fil.EndsWith(".mpg") OrElse Fil.EndsWith(".mpeg") OrElse Fil.EndsWith(".mov") OrElse Fil.EndsWith(".qt") OrElse Fil.EndsWith(".asf") OrElse Fil.EndsWith(".wmv") OrElse Fil.EndsWith(".dvr-ms") OrElse Fil.EndsWith(".m1v") OrElse Fil.EndsWith(".mpe") OrElse Fil.EndsWith(".wma") OrElse Fil.EndsWith(".wm") OrElse Fil.EndsWith(".wav") OrElse Fil.EndsWith(".wpl") OrElse Fil.EndsWith(".mp3") OrElse Fil.EndsWith(".m3u") OrElse Fil.EndsWith(".flac") Then
                                        CurrentCountVideosStep += 1
                                    ElseIf Fil.EndsWith(".txt") Then
                                        CurrentCountVideosStep += 1
                                    ElseIf Fil.EndsWith(".html") OrElse Fil.EndsWith(".htm") OrElse Fil.EndsWith(".xhtm") Then
                                        CurrentCountVideosStep += 1
                                    End If

                                    If CurrentCountVideos = strVideos.Count Then
                                        isFinished = True
                                    Else
                                        CurExt = GetExt(strVideos(CurrentCountVideos))
                                    End If
                                Loop
                            End If
                            '/Just to count how many Videos are there

                            If CurrentCountImages = strImages.Count AndAlso Not VideosAvailable Then
                                btnNext.Text = strLanguage_Presentation(3)     'Finish
                            ElseIf CurrentCountImages <> strImages.Count Then
                                btnNext.Text = strLanguage_Presentation(1)     'Next
                            Else
                                btnNext.Text = strLanguage_Presentation(7) 'Go to Videos
                            End If

                        Else
                            WorkingOnImages = False
                            WorkingOnVideos = True
                        End If

                        If (VideosAvailable AndAlso WorkingOnVideos AndAlso Not WorkingOnImages) OrElse Not ImagesAvailable Then
                            WorkingOnVideos = True
                            Dim CurExt As String = GetExt(strVideos(CurrentCountVideos))
                            Dim isFinished As Boolean = False

                            Do Until isFinished OrElse isContained(CurExt, AlreadyLoadedVideoExt)
                                CurFileNameAlone = GetFileNameAlone(strVideos(CurrentCountVideos))
                                AlreadyLoadedVideoExt.Add(CurExt)

                                Dim Fil As String = strVideos(CurrentCountVideos).ToLower
                                If Fil.EndsWith(".avi") OrElse Fil.EndsWith(".mp4") OrElse Fil.EndsWith(".mpg") OrElse Fil.EndsWith(".mpeg") OrElse Fil.EndsWith(".mov") OrElse Fil.EndsWith(".qt") OrElse Fil.EndsWith(".asf") OrElse Fil.EndsWith(".wmv") OrElse Fil.EndsWith(".dvr-ms") OrElse Fil.EndsWith(".m1v") OrElse Fil.EndsWith(".mpe") OrElse Fil.EndsWith(".wma") OrElse Fil.EndsWith(".wm") OrElse Fil.EndsWith(".wav") OrElse Fil.EndsWith(".wpl") OrElse Fil.EndsWith(".mp3") OrElse Fil.EndsWith(".m3u") OrElse Fil.EndsWith(".flac") Then
                                    wmpMedia.Ctlcontrols.stop()
                                    wmpMedia.URL = strVideos(CurrentCountVideos)
                                    CurrentCountVideos += 1
                                    CurrentCountVideosStep += 1
                                    picInfo.Visible = False
                                    wmpMedia.Visible = True
                                    wmpMedia.Ctlcontrols.play()

                                ElseIf strVideos(CurrentCountVideos).ToLower.EndsWith(".txt") Then
                                    ReadFile(strVideos(CurrentCountVideos), txtInfo)
                                    CurrentCountVideos += 1
                                    CurrentCountVideosStep += 1
                                    txtInfo.Visible = True
                                    wbInfo.Visible = False

                                ElseIf strVideos(CurrentCountVideos).ToLower.EndsWith(".html") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".htm") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".xhtm") Then
                                    wbInfo.Navigate(strVideos(CurrentCountVideos))
                                    CurrentCountVideos += 1
                                    CurrentCountVideosStep += 1
                                    txtInfo.Visible = False
                                    wbInfo.Visible = True
                                End If

                                If CurrentCountVideos = strVideos.Count Then
                                    isFinished = True
                                Else
                                    CurExt = GetExt(strVideos(CurrentCountVideos))
                                End If

                            Loop

                            'Just to Count how many they are
                            If ImagesAvailable Then
                                Do Until isFinished OrElse isContained(CurExt, AlreadyLoadedImageExt)
                                    AlreadyLoadedImageExt.Add(CurExt)
                                    If strImages(CurrentCountImages).ToLower.EndsWith(".gif") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".jpg") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".jpeg") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".bmp") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".wmf") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".png") Then
                                        CurrentCountImagesStep += 1
                                    ElseIf strImages(CurrentCountImages).ToLower.EndsWith(".txt") Then
                                        CurrentCountImagesStep += 1
                                    ElseIf strImages(CurrentCountImages).ToLower.EndsWith(".html") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".htm") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".xhtm") Then
                                        CurrentCountImagesStep += 1
                                    End If

                                    If CurrentCountImages = strImages.Count Then
                                        isFinished = True
                                    Else
                                        CurExt = GetExt(strImages(CurrentCountImages))
                                    End If
                                Loop
                            End If
                            'Just to Count how many they are


                            If CurrentCountVideos = strVideos.Count AndAlso Not ImagesAvailable Then
                                btnNext.Text = strLanguage_Presentation(3)     'Finish
                            ElseIf CurrentCountVideos <> strVideos.Count Then
                                btnNext.Text = strLanguage_Presentation(1)     'Next
                            Else
                                btnNext.Text = strLanguage_Presentation(7) 'Go to Videos
                            End If

                        End If

                        If IsNumeric(CurFileNameAlone(0)) AndAlso CurFileNameAlone.Length > 1 AndAlso CurFileNameAlone.Contains(" "c) Then
                            Dim StartIndex As Integer = CurFileNameAlone.IndexOf(" "c) + 1
                            If StartIndex < CurFileNameAlone.Length Then
                                CurFileNameAlone = Mid(CurFileNameAlone, StartIndex + 1, CurFileNameAlone.Length - StartIndex + 1)
                            End If
                        End If
                        If CurrentCountImagesStep = 0 Then CurrentCountImagesStep = 1
                        If CurrentCountVideosStep = 0 Then CurrentCountVideosStep = 1
                        Text = MeText & " " & ((CurrentCountImages / CurrentCountImagesStep) + (CurrentCountVideos / CurrentCountVideosStep) &
                                    " / " & ((strImages.Count / CurrentCountImagesStep) + (strVideos.Count / CurrentCountVideosStep))) & " - " & CurFileNameAlone

                    Else
                        If Not isFirstTimeRun() Then MsgBox(strLanguage_Presentation(4), MsgBoxStyle.Critical) 'There are no available files to present.
                        Close()
                    End If

                Else
                    If Not isFirstTimeRun() Then MsgBox(strLanguage_Presentation(4), MsgBoxStyle.Critical) 'There are no available files to present.
                    Close()
                End If

            Else
                If Not isFirstTimeRun() Then MsgBox(strLanguage_Presentation(4), MsgBoxStyle.Critical) 'There are no available files to present.
                Close()
            End If

        Catch ex As Exception
            CreateCrashFile(ex, True)
        End Try
    End Sub

    Private Sub btnPrevious_Click(sender As System.Object, e As EventArgs) Handles btnPrevious.Click
        If WorkingOnImages Then
            Call GoToPreviousImage()
            btnPrevious.Focus()
        Else
            Call GoToPreviousVideo()
            btnPrevious.Focus()
        End If
    End Sub

    Private Sub GoToPreviousVideo() 'No need for "Handles"
        If CurrentCountVideos / CurrentCountVideosStep <> 1 Then     'Else it is the begining
            If CurrentCountVideos - CurrentCountVideosStep = strVideos.Count AndAlso CurrentCountImages = 0 AndAlso ImagesAvailable Then
                btnNext.Text = strLanguage_Presentation(8)      'Go to Images
            ElseIf CurrentCountVideos - CurrentCountVideosStep = strVideos.Count AndAlso CurrentCountImages = strImages.Count Then
                btnNext.Text = strLanguage_Presentation(4)      '&Finish
            Else
                btnNext.Text = strLanguage_Presentation(1)      'Next
            End If

            CurrentCountVideos -= (CurrentCountVideosStep + 2)
            CurrentCountVideosStep = 0

            If CurrentCountVideos - CurrentCountVideosStep = 0 AndAlso CurrentCountImages = strImages.Count AndAlso strImages.Count <> 0 Then
                btnPrevious.Text = strLanguage_Presentation(8)  'Go to Images
            Else
                btnPrevious.Text = strLanguage_Presentation(6)  'Previous
            End If

            Dim AlreadyLoadedVideoExt As New List(Of String)
            Dim CurExt As String = GetExt(strVideos(CurrentCountVideos))
            Dim isFinished As Boolean = False

            Do Until isFinished OrElse isContained(CurExt, AlreadyLoadedVideoExt)
                AlreadyLoadedVideoExt.Add(CurExt)
                CurFileNameAlone = GetFileNameAlone(strVideos(CurrentCountVideos))

                If strVideos(CurrentCountVideos).ToLower.EndsWith(".avi") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".mp4") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".mpg") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".mpeg") Then
                    wmpMedia.Ctlcontrols.stop()
                    wmpMedia.URL = strVideos(CurrentCountVideos)
                    CurrentCountVideos += 1
                    CurrentCountVideosStep += 1
                    picInfo.Visible = False
                    wmpMedia.Visible = True
                    wmpMedia.Ctlcontrols.play()

                ElseIf strVideos(CurrentCountVideos).ToLower.EndsWith(".txt") Then
                    ReadFile(strVideos(CurrentCountVideos), txtInfo)
                    CurrentCountVideos += 1
                    CurrentCountVideosStep += 1
                    txtInfo.Visible = True
                    wbInfo.Visible = False

                ElseIf strVideos(CurrentCountVideos).ToLower.EndsWith(".html") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".htm") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".xhtm") Then
                    wbInfo.Navigate(strVideos(CurrentCountVideos))
                    CurrentCountVideos += 1
                    CurrentCountVideosStep += 1
                    txtInfo.Visible = False
                    wbInfo.Visible = True
                End If

                If CurrentCountVideos = strVideos.Count Then
                    isFinished = True
                Else
                    CurExt = GetExt(strVideos(CurrentCountVideos))
                End If

            Loop

            If IsNumeric(CurFileNameAlone(0)) AndAlso CurFileNameAlone.Length > 1 AndAlso CurFileNameAlone.Contains(" "c) Then
                Dim StartIndex As Integer = CurFileNameAlone.IndexOf(" "c) + 1
                If StartIndex < CurFileNameAlone.Length Then
                    CurFileNameAlone = Mid(CurFileNameAlone, StartIndex + 1, CurFileNameAlone.Length - StartIndex + 1)
                End If
            End If
            If CurrentCountImagesStep = 0 Then CurrentCountImagesStep = 1
            If CurrentCountVideosStep = 0 Then CurrentCountVideosStep = 1
            Text = MeText & " " & ((CurrentCountImages / CurrentCountImagesStep) + (CurrentCountVideos / CurrentCountVideosStep) &
                        " / " & ((strImages.Count / CurrentCountImagesStep) + (strVideos.Count / CurrentCountVideosStep))) & " - " & CurFileNameAlone

        ElseIf ImagesAvailable AndAlso CurrentCountImages = strImages.Count Then
            WorkingOnImages = True
            WorkingOnVideos = False
            wmpMedia.Ctlcontrols.stop()
            CurrentCountImages += CurrentCountImagesStep
            CurrentCountVideos = 0
            Call GoToPreviousImage()
        Else
            MsgBox(strLanguage_Presentation(5), MsgBoxStyle.Information)    'There are no previous data
        End If
    End Sub

    Public Sub GoToPreviousImage() 'No need for "Handles"
        If CurrentCountImages / CurrentCountImagesStep <> 1 Then     'Else it is the begining

            If CurrentCountImages - CurrentCountImagesStep = strImages.Count AndAlso CurrentCountVideos = 0 AndAlso VideosAvailable Then
                btnNext.Text = strLanguage_Presentation(7)          'Go to Videos
            ElseIf CurrentCountImages - CurrentCountImagesStep = strImages.Count AndAlso CurrentCountVideos = strVideos.Count Then
                btnNext.Text = strLanguage_Presentation(3)          'Finish
            Else
                btnNext.Text = strLanguage_Presentation(1)          'Next
            End If

            CurrentCountImages -= (CurrentCountImagesStep + 2)
            CurrentCountImagesStep = 0

            If CurrentCountImages = 0 AndAlso CurrentCountVideos = strVideos.Count AndAlso VideosAvailable Then
                btnPrevious.Text = strLanguage_Presentation(7)      'Go to Videos
            Else
                btnPrevious.Text = strLanguage_Presentation(6)      'Previous
            End If

            Dim AlreadyLoadedImageExt As New List(Of String)
            Dim CurExt As String = GetExt(strImages(CurrentCountImages))
            Dim isFinished As Boolean = False

            Do Until isFinished OrElse isContained(CurExt, AlreadyLoadedImageExt)
                CurFileNameAlone = GetFileNameAlone(strImages(CurrentCountImages))
                AlreadyLoadedImageExt.Add(CurExt)

                If strImages(CurrentCountImages).ToLower.EndsWith(".gif") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".jpg") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".jpeg") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".bmp") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".wmf") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".png") Then
                    picInfo.BackgroundImageLayout = ImageLayout.None
                    picInfo.BackgroundImage = Image.FromFile(strImages(CurrentCountImages))
                    CurrentCountImages += 1
                    CurrentCountImagesStep += 1
                    picInfo.Visible = True
                    wmpMedia.Visible = False
                ElseIf strImages(CurrentCountImages).ToLower.EndsWith(".txt") Then
                    ReadFile(strImages(CurrentCountImages), txtInfo)
                    CurrentCountImages += 1
                    CurrentCountImagesStep += 1
                    txtInfo.Visible = True
                    wbInfo.Visible = False
                ElseIf strImages(CurrentCountImages).ToLower.EndsWith(".html") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".htm") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".xhtm") Then
                    wbInfo.Navigate(strImages(CurrentCountImages))
                    CurrentCountImages += 1
                    CurrentCountImagesStep += 1
                    txtInfo.Visible = False
                    wbInfo.Visible = True
                End If

                If CurrentCountImages = strImages.Count Then
                    isFinished = True
                Else
                    CurExt = GetExt(strImages(CurrentCountImages))
                End If

            Loop

            If IsNumeric(CurFileNameAlone(0)) AndAlso CurFileNameAlone.Length > 1 AndAlso CurFileNameAlone.Contains(" "c) Then
                Dim StartIndex As Integer = CurFileNameAlone.IndexOf(" "c) + 1
                If StartIndex < CurFileNameAlone.Length Then
                    CurFileNameAlone = Mid(CurFileNameAlone, StartIndex + 1, CurFileNameAlone.Length - StartIndex + 1)
                End If
            End If
            If CurrentCountImagesStep = 0 Then CurrentCountImagesStep = 1
            If CurrentCountVideosStep = 0 Then CurrentCountVideosStep = 1
            Text = MeText & " " & ((CurrentCountImages / CurrentCountImagesStep) + (CurrentCountVideos / CurrentCountVideosStep) &
                        " / " & ((strImages.Count / CurrentCountImagesStep) + (strVideos.Count / CurrentCountVideosStep))) & " - " & CurFileNameAlone

        ElseIf VideosAvailable AndAlso CurrentCountVideos = strVideos.Count Then
            WorkingOnImages = False
            WorkingOnVideos = True
            wmpMedia.Ctlcontrols.stop()
            CurrentCountVideos += CurrentCountVideosStep
            CurrentCountImages = 0
            Call GoToPreviousVideo()
        Else
            MsgBox(strLanguage_Presentation(5), MsgBoxStyle.Information)    'There are no previous data
        End If

    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnNext.Click
        If WorkingOnImages Then
            Call GoToNextImage()
            btnNext.Focus()
        Else
            Call GoToNextVideo()
            btnNext.Focus()
        End If
    End Sub

    Private Sub GoToNextImage()
        If CurrentCountImages <> strImages.Count Then     'Else it is the last item (end)
            CurrentCountImagesStep = 0

            If CurrentCountImages = 0 AndAlso VideosAvailable AndAlso CurrentCountVideos = strVideos.Count Then
                btnPrevious.Text = strLanguage_Presentation(7)      'Go to Videos
            Else
                btnPrevious.Text = strLanguage_Presentation(6)      'Previous
            End If

            Dim AlreadyLoadedImageExt As New List(Of String)
            Dim CurExt As String = GetExt(strImages(CurrentCountImages))
            Dim isFinished As Boolean = False

            Do Until isFinished OrElse isContained(CurExt, AlreadyLoadedImageExt)
                CurFileNameAlone = GetFileNameAlone(strImages(CurrentCountImages))
                AlreadyLoadedImageExt.Add(CurExt)

                If strImages(CurrentCountImages).ToLower.EndsWith(".gif") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".jpg") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".jpeg") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".bmp") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".wmf") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".png") Then
                    picInfo.BackgroundImageLayout = ImageLayout.None
                    picInfo.BackgroundImage = Image.FromFile(strImages(CurrentCountImages))
                    CurrentCountImages += 1
                    CurrentCountImagesStep += 1
                    picInfo.Visible = True
                    wmpMedia.Visible = False
                ElseIf strImages(CurrentCountImages).ToLower.EndsWith(".txt") Then
                    ReadFile(strImages(CurrentCountImages), txtInfo)
                    CurrentCountImages += 1
                    CurrentCountImagesStep += 1
                    txtInfo.Visible = True
                    wbInfo.Visible = False
                ElseIf strImages(CurrentCountImages).ToLower.EndsWith(".html") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".htm") OrElse strImages(CurrentCountImages).ToLower.EndsWith(".xhtm") Then
                    wbInfo.Navigate(strImages(CurrentCountImages))
                    CurrentCountImages += 1
                    CurrentCountImagesStep += 1
                    txtInfo.Visible = False
                    wbInfo.Visible = True
                End If

                If CurrentCountImages = strImages.Count Then
                    isFinished = True
                Else
                    CurExt = GetExt(strImages(CurrentCountImages))
                End If

            Loop

            If CurrentCountImages = strImages.Count AndAlso (Not VideosAvailable OrElse CurrentCountVideos = strVideos.Count) Then
                btnNext.Text = strLanguage_Presentation(3)      'Finish
            ElseIf CurrentCountImages = strImages.Count AndAlso VideosAvailable AndAlso CurrentCountVideos = 0 Then
                btnNext.Text = strLanguage_Presentation(7)      'Go to Videos
            Else
                btnNext.Text = strLanguage_Presentation(1)      'Next
            End If

            If IsNumeric(CurFileNameAlone(0)) AndAlso CurFileNameAlone.Length > 1 AndAlso CurFileNameAlone.Contains(" "c) Then
                Dim StartIndex As Integer = CurFileNameAlone.IndexOf(" "c) + 1
                If StartIndex < CurFileNameAlone.Length Then
                    CurFileNameAlone = Mid(CurFileNameAlone, StartIndex + 1, CurFileNameAlone.Length - StartIndex + 1)
                End If
            End If
            If CurrentCountImagesStep = 0 Then CurrentCountImagesStep = 1
            If CurrentCountVideosStep = 0 Then CurrentCountVideosStep = 1
            Text = MeText & " " & ((CurrentCountImages / CurrentCountImagesStep) + (CurrentCountVideos / CurrentCountVideosStep) &
                        " / " & ((strImages.Count / CurrentCountImagesStep) + (strVideos.Count / CurrentCountVideosStep))) & " - " & CurFileNameAlone

        ElseIf VideosAvailable AndAlso CurrentCountVideos = 0 Then
            WorkingOnImages = False
            WorkingOnVideos = True
            CurrentCountImages = strImages.Count
            Call GoToNextVideo()
        Else
            Close()
        End If

    End Sub

    Private Async Sub GoToNextVideo()
        If CurrentCountVideos <> strVideos.Count Then     'Else it is the last item (end)
            CurrentCountVideosStep = 0

            If CurrentCountVideos = 0 AndAlso CurrentCountImages = strImages.Count AndAlso ImagesAvailable Then
                btnPrevious.Text = strLanguage_Presentation(8)  'Go to Images
            Else
                btnPrevious.Text = strLanguage_Presentation(6)  'Previous
            End If

            Dim AlreadyLoadedVideoExt As New List(Of String)
            Dim CurExt As String = GetExt(strVideos(CurrentCountVideos))
            Dim isFinished As Boolean = False

            Do Until isFinished OrElse isContained(CurExt, AlreadyLoadedVideoExt)
                CurFileNameAlone = GetFileNameAlone(strVideos(CurrentCountVideos))
                AlreadyLoadedVideoExt.Add(CurExt)

                If strVideos(CurrentCountVideos).ToLower.EndsWith(".avi") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".mp4") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".mpg") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".mpeg") Then
                    wmpMedia.Ctlcontrols.stop()
                    wmpMedia.URL = strVideos(CurrentCountVideos)
                    CurrentCountVideos += 1
                    CurrentCountVideosStep += 1
                    picInfo.Visible = False
                    wmpMedia.Visible = True
                    Await Task.Run(
                        Sub()
                            Thread.Sleep(500)
                        End Sub)
                    wmpMedia.Ctlcontrols.play()

                ElseIf strVideos(CurrentCountVideos).ToLower.EndsWith(".txt") Then
                    ReadFile(strVideos(CurrentCountVideos), txtInfo)
                    CurrentCountVideos += 1
                    CurrentCountVideosStep += 1
                    txtInfo.Visible = True
                    wbInfo.Visible = False

                ElseIf strVideos(CurrentCountVideos).ToLower.EndsWith(".html") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".htm") OrElse strVideos(CurrentCountVideos).ToLower.EndsWith(".xhtm") Then
                    wbInfo.Navigate(strVideos(CurrentCountVideos))
                    CurrentCountVideos += 1
                    CurrentCountVideosStep += 1
                    txtInfo.Visible = False
                    wbInfo.Visible = True
                End If

                If CurrentCountVideos = strVideos.Count Then
                    isFinished = True
                Else
                    CurExt = GetExt(strVideos(CurrentCountVideos))
                End If

            Loop


            If CurrentCountVideos = strVideos.Count AndAlso (Not ImagesAvailable OrElse CurrentCountImages = strImages.Count) Then
                btnNext.Text = strLanguage_Presentation(3)      'Finish
            ElseIf CurrentCountVideos = strVideos.Count AndAlso ImagesAvailable AndAlso CurrentCountImages = 0 Then
                btnNext.Text = strLanguage_Presentation(8)      'Go to Images
            Else
                btnNext.Text = strLanguage_Presentation(1)      'Next
            End If

            If IsNumeric(CurFileNameAlone(0)) AndAlso CurFileNameAlone.Length > 1 AndAlso CurFileNameAlone.Contains(" "c) Then
                Dim StartIndex As Integer = CurFileNameAlone.IndexOf(" "c) + 1
                If StartIndex < CurFileNameAlone.Length Then
                    CurFileNameAlone = Mid(CurFileNameAlone, StartIndex + 1, CurFileNameAlone.Length - StartIndex + 1)
                End If
            End If
            If CurrentCountImagesStep = 0 Then CurrentCountImagesStep = 1
            If CurrentCountVideosStep = 0 Then CurrentCountVideosStep = 1
            Text = MeText & " " & ((CurrentCountImages / CurrentCountImagesStep) + (CurrentCountVideos / CurrentCountVideosStep) &
                        " / " & ((strImages.Count / CurrentCountImagesStep) + (strVideos.Count / CurrentCountVideosStep))) & " - " & CurFileNameAlone

        ElseIf ImagesAvailable AndAlso CurrentCountImages = 0 Then
            WorkingOnImages = True
            WorkingOnVideos = False
            wmpMedia.Ctlcontrols.stop()
            CurrentCountVideos = strVideos.Count
            Call GoToNextImage()
        Else
            Close()
        End If

    End Sub

    Private Sub picInfo_Resize(sender As Object, e As EventArgs) Handles picInfo.BackgroundImageChanged
        If Not isFormResizing AndAlso picInfo.BackgroundImage IsNot Nothing Then

            Dim ProposedWidth As Integer = picInfo.Location.X + picInfo.BackgroundImage.Size.Width + 28
            If ProposedWidth < 600 Then
                ProposedWidth = 600
            End If

            Dim ProposedHeight As Integer = scTextMedia.SplitterDistance + picInfo.BackgroundImage.Size.Height + 28 + btnNext.Size.Height + 50
            If ProposedHeight < 500 Then
                ProposedHeight = 500
            End If

            Size = New Size(ProposedWidth, ProposedHeight)

            txtInfo.Size = New Size(Size.Width - 40, txtInfo.Size.Height)

            btnExit.Location = New Point(12, Size.Height - btnExit.Size.Height - 50)
            btnNext.Location = New Point(Size.Width - btnNext.Size.Width - 28, Size.Height - btnNext.Size.Height - 50)
            btnPrevious.Location = New Point(btnNext.Location.X - btnPrevious.Size.Width - 6, Size.Height - btnPrevious.Size.Height - 50)

            If Top + Height > My.Computer.Screen.Bounds.Height Then
                Dim ProposedTop As Integer = Top - (Top + Height - My.Computer.Screen.Bounds.Height)
                If ProposedTop > 0 Then
                    Top = ProposedTop
                Else
                    Top = 0
                    Height = My.Computer.Screen.Bounds.Height
                End If
            End If

            If Left + Width > My.Computer.Screen.Bounds.Width Then
                Dim ProposedLeft As Integer = Left - (Left + Width - My.Computer.Screen.Bounds.Width)
                If ProposedLeft > 0 Then
                    Left = ProposedLeft
                Else
                    Left = 0
                    Width = My.Computer.Screen.Bounds.Width
                End If
            End If


            picInfo.BackgroundImageLayout = ImageLayout.Stretch
        End If


    End Sub

    Private Sub frmPresentation_ResizeBegin(sender As Object, e As EventArgs) Handles Me.ResizeBegin
        isFormResizing = True
        picInfo.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub frmPresentation_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        isFormResizing = False
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

    Private Sub wmpMedia_PlayStateChange(sender As Object, e As AxWMPLib._WMPOCXEvents_PlayStateChangeEvent) Handles wmpMedia.PlayStateChange
        If e.newState = 8 Then Call GoToNextVideo()
    End Sub
End Class