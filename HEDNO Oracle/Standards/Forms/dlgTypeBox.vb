'Version 2.3 2016-07-18
'Added support for Date() (Array)
'Critical Fix: On languages that didn't separate the decimal points with a dot, the returned number(s) was wrong.
'Critical Fix with Decimal numbers on languages with "," decimal separator; Fix on AllowNothingAsResult; Fixed a bug with Multiline Text
'Needs Language version 1.5
Option Strict On

Imports System.Globalization
Imports System.Threading.Thread

Public Enum TypeMode As Integer
    _Text = 1
    _Decimal = 2
    _Double = 3
    _Single = 4
    _Integer = 5
    _UInteger = 6
    _Short = 7
    _UShort = 8
    _Long = 9
    _ULong = 10
    _Date = 11
    _ComboBox = 12
End Enum

Public Class dlgTypeBox
#Region "Constants And Variables"
    Public strLanguage_Typebox() As String

    Dim CurCulture As String = CurrentThread.CurrentCulture.Name
    Dim CurCultureUI As String = CurrentThread.CurrentUICulture.Name

    Dim strUserInput As String = String.Empty
    Dim strUserInputs() As String
    Dim strLowerLimit, strUpperLimit As String
    Dim strLowerLimitPerLine, strUpperLimitPerLine As String

    Public Title As String = ""
    Public isRange As Boolean = False
    Public LabelText As String = ""
    Public MultiLine As Boolean = False
    Public FormWidth As Integer = 0
    Public FormHeight As Integer = 0
    Public DateFormat As String = ""
    Public TypeBoxMode As Integer = 0
    Public ValidDateTo As Date = DefaultDate
    Public PreLoadText() As String = Nothing
    Public ValidDateFrom As Date = DefaultDate
    Public isNumericOnly As Boolean = False
    Public doEraseNullLines As Boolean = False    'On Arrays
    Public MustntStartWthNum As Boolean = False
    Public ComboBoxDataSource As Object = Nothing
    Public AllowNothingAsResult As Boolean
    Public RoundNum As Integer = -1
    Public MinNumLength As Integer = -1
    Public AtLeastOneBecameZeroByRounding As Boolean
    Public WholeRangeMustChange As Boolean


    Public MinimumValidTextSizePerLine As Double = -1
    Public MaximumValidTextSizePerLine As Double = -1

    Public MinimumValidTextSize As Double = -1
    Public MaximumValidTextSize As Double = -1

    Public MinimumValidDecimal As Decimal = Decimal.MinValue
    Public MaximumValidDecimal As Decimal = Decimal.MinValue

    Public MinimumValidDouble As Double = Double.MinValue
    Public MaximumValidDouble As Double = Double.MinValue

    Public MinimumValidSingle As Single = Single.MinValue
    Public MaximumValidSingle As Single = Single.MinValue

    Public MinimumValidInteger As Integer = Integer.MinValue
    Public MaximumValidInteger As Integer = Integer.MinValue

    Public MinimumValidUInteger As UInteger = UInteger.MaxValue
    Public MaximumValidUInteger As UInteger = UInteger.MaxValue

    Public MinimumValidShort As Short = Short.MinValue
    Public MaximumValidShort As Short = Short.MinValue

    Public MinimumValidUShort As UShort = UShort.MaxValue
    Public MaximumValidUShort As UShort = UShort.MaxValue

    Public MinimumValidLong As Long = Long.MinValue
    Public MaximumValidLong As Long = Long.MinValue

    Public MinimumValidULong As ULong = ULong.MaxValue
    Public MaximumValidULong As ULong = ULong.MaxValue

    'Used to RETURN VALUES!
    Public ReturnedStrDoubleOne As String
    Public ReturnedStrDoubleTwo As String
    Public ReturnedStrDoubleArrayOne() As String
    Public ReturnedStrDoubleArrayTwo() As String

    Public ReturnedDecimalOne As Decimal
    Public ReturnedDecimalTwo As Decimal
    Public ReturnedDecimalArrayOne() As Decimal
    Public ReturnedDecimalArrayTwo() As Decimal

    Public ReturnedDoubleOne As Double
    Public ReturnedDoubleTwo As Double
    Public ReturnedDoubleArrayOne() As Double
    Public ReturnedDoubleArrayTwo() As Double

    Public ReturnedSingleOne As Single
    Public ReturnedSingleTwo As Single
    Public ReturnedSingleArrayOne() As Single
    Public ReturnedSingleArrayTwo() As Single

    Public ReturnedIntegerOne As Integer
    Public ReturnedIntegerTwo As Integer
    Public ReturnedIntegerArrayOne() As Integer
    Public ReturnedIntegerArrayTwo() As Integer

    Public ReturnedUIntegerOne As UInteger
    Public ReturnedUIntegerTwo As UInteger
    Public ReturnedUIntegerArrayOne() As UInteger
    Public ReturnedUIntegerArrayTwo() As UInteger

    Public ReturnedShortOne As Short
    Public ReturnedShortTwo As Short
    Public ReturnedShortArrayOne() As Short
    Public ReturnedShortArrayTwo() As Short

    Public ReturnedUShortOne As UShort
    Public ReturnedUShortTwo As UShort
    Public ReturnedUShortArrayOne() As UShort
    Public ReturnedUShortArrayTwo() As UShort

    Public ReturnedLongOne As Long
    Public ReturnedLongTwo As Long
    Public ReturnedLongArrayOne() As Long
    Public ReturnedLongArrayTwo() As Long

    Public ReturnedULongOne As ULong
    Public ReturnedULongTwo As ULong
    Public ReturnedULongArrayOne() As ULong
    Public ReturnedULongArrayTwo() As ULong

    Public ReturnedDateArrayOne() As Date

#End Region

    Private Sub dlgTypeBox_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CurCulture)
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(CurCultureUI)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TypeBox_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call Typebox_Language(Me)

        Call frmSkin(Me, False)

        If Title <> "" Then
            Text = Title
        End If

        If LabelText <> "" Then
            lblInfo.Visible = True
            lblInfo.Text = LabelText
        End If

        If MultiLine Then
            txtInput.Multiline = True
            txtInput.AcceptsReturn = True
            FormBorderStyle = FormBorderStyle.Sizable
            FormBorderStyle = FormBorderStyle.Sizable
            If FormHeight = 0 Then
                Height = Height * 2
            Else
                Height = FormHeight
            End If

        Else
            txtInput.Multiline = False
            txtInput.AcceptsReturn = False
            If FormHeight <> 0 Then
                Height = FormHeight
            End If
            FormBorderStyle = FormBorderStyle.FixedDialog

        End If

        If PreLoadText IsNot Nothing Then
            txtInput.Lines = PreLoadText
            txtInput.SelectAll()
        End If

        If FormWidth <> 0 Then
            Width = FormWidth
        End If

        If TypeBoxMode = TypeMode._Text Then
            txtInput.Visible = True
            txtInput.Focus()

            If isNumericOnly Then
                CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
                CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
                MinimumValidDouble = MinimumValidTextSizePerLine
                MaximumValidDouble = MaximumValidTextSizePerLine

                If MinimumValidTextSize <> Double.MinValue OrElse MaximumValidTextSize <> Double.MinValue Then
                    lblValidRange.Visible = True

                    lblValidRange.Text = strLanguage_Typebox(35)    'Text's Characters Bounds:
                    If MinimumValidTextSize <> Double.MinValue Then
                        lblValidRange.Text &= "[ " & MinimumValidTextSize & " , "
                        strLowerLimit = CStr(MinimumValidTextSize)
                    Else
                        lblValidRange.Text &= "[ " & strLanguage_Typebox(33) & " , " '-Infinity
                        strLowerLimit = strLanguage_Typebox(33)
                    End If
                    If MaximumValidTextSize <> Double.MinValue Then
                        lblValidRange.Text &= MaximumValidTextSize & " ]"
                        strUpperLimit = CStr(MaximumValidTextSize)
                    Else
                        lblValidRange.Text &= strLanguage_Typebox(34) & " ]" '+Infinity
                        strUpperLimit = strLanguage_Typebox(34)
                    End If
                End If

                If MinimumValidDouble <> Double.MinValue OrElse MaximumValidDouble <> Double.MinValue Then
                    lblPerLineBounds.Visible = True

                    lblPerLineBounds.Text = strLanguage_Typebox(31)    'Valid Range:
                    If MinimumValidDouble <> Double.MinValue Then
                        lblPerLineBounds.Text &= "[ " & MinimumValidDouble & " , "
                        strLowerLimit = CStr(MinimumValidDouble)
                    Else
                        lblPerLineBounds.Text &= "[ " & strLanguage_Typebox(33) & " , " '-Infinity
                        strLowerLimit = strLanguage_Typebox(33)
                    End If
                    If MaximumValidDouble <> Double.MinValue Then
                        lblPerLineBounds.Text &= MaximumValidDouble & " ]"
                        strUpperLimit = CStr(MaximumValidDouble)
                    Else
                        lblPerLineBounds.Text &= strLanguage_Typebox(34) & " ]" '+Infinity
                        strUpperLimit = strLanguage_Typebox(34)
                    End If
                End If

            Else
                'If either or both of the two bounds are set
                If (MinimumValidTextSize <> -1 AndAlso MinimumValidTextSize <> Double.MinValue) OrElse (MaximumValidTextSize <> -1 AndAlso MaximumValidTextSize <> Double.MinValue) Then
                    lblValidRange.Visible = True

                    If MinimumValidTextSize <> -1 AndAlso MinimumValidTextSize <> Double.MinValue AndAlso MaximumValidTextSize <> -1 AndAlso MaximumValidTextSize <> Double.MinValue Then
                        'If both lower and upper bounds are set
                        lblValidRange.Text = strLanguage_Typebox(35) & "[" & MinimumValidTextSize & ", " & MaximumValidTextSize & "]" 'Text's Characters Bounds:
                        strLowerLimit = CStr(MinimumValidTextSize)
                        strUpperLimit = CStr(MaximumValidTextSize)

                    ElseIf MaximumValidTextSize <> -1 AndAlso MaximumValidTextSize <> Double.MinValue Then
                        'If only the upper bound is set
                        Dim tmpLowerLimit As String
                        If AllowNothingAsResult Then tmpLowerLimit = "0" Else tmpLowerLimit = "1"
                        lblValidRange.Text = strLanguage_Typebox(35) & "[" & tmpLowerLimit & ", " & MaximumValidTextSize & "]" 'Text's Characters Bounds:
                        strLowerLimit = tmpLowerLimit
                        strUpperLimit = CStr(MaximumValidTextSize)

                    ElseIf MaximumValidTextSize <> -1 AndAlso MaximumValidTextSize <> Double.MinValue Then
                        'If only the lower bound is set
                        lblValidRange.Text = strLanguage_Typebox(35) & "[" & MinimumValidTextSize & ", " & strLanguage_Typebox(34) & " ]" 'Text's Characters Bounds:   '+Infinity
                        strLowerLimit = CStr(MinimumValidTextSize)
                        strUpperLimit = strLanguage_Typebox(34) '+Infinity
                    End If
                End If

                If (MinimumValidTextSizePerLine <> -1 AndAlso MinimumValidTextSizePerLine <> Double.MinValue) OrElse (MaximumValidTextSizePerLine <> -1 AndAlso MaximumValidTextSizePerLine <> Double.MinValue) Then
                    lblPerLineBounds.Visible = True

                    If MinimumValidTextSizePerLine <> -1 AndAlso MinimumValidTextSizePerLine <> Double.MinValue AndAlso MaximumValidTextSizePerLine <> -1 AndAlso MaximumValidTextSizePerLine <> Double.MinValue Then
                        'If both lower and upper bounds are set
                        lblPerLineBounds.Text = strLanguage_Typebox(36) & "[" & MinimumValidTextSizePerLine & ", " & MaximumValidTextSizePerLine & "]" 'Per Line Characters Bounds:
                        strLowerLimitPerLine = CStr(MinimumValidTextSizePerLine)
                        strUpperLimitPerLine = CStr(MaximumValidTextSizePerLine)

                    ElseIf MaximumValidTextSizePerLine <> -1 AndAlso MaximumValidTextSizePerLine <> Double.MinValue Then
                        'If only the upper bound is set
                        Dim tmpLowerLimitPerLine As String
                        If AllowNothingAsResult Then tmpLowerLimitPerLine = "0" Else tmpLowerLimitPerLine = "1"
                        lblPerLineBounds.Text = strLanguage_Typebox(36) & "[" & tmpLowerLimitPerLine & ", " & MaximumValidTextSizePerLine & "]" 'Per Line Characters Bounds:
                        strLowerLimitPerLine = tmpLowerLimitPerLine
                        strUpperLimitPerLine = CStr(MaximumValidTextSizePerLine)

                    ElseIf MaximumValidTextSizePerLine <> -1 AndAlso MaximumValidTextSizePerLine <> Double.MinValue Then
                        'If only the lower bound is set
                        lblPerLineBounds.Text = strLanguage_Typebox(36) & "[" & MinimumValidTextSizePerLine & ", " & strLanguage_Typebox(34) & " ]" 'Per Line Characters Bounds:  '+Infinity
                        strLowerLimitPerLine = CStr(MinimumValidTextSizePerLine)
                        strUpperLimitPerLine = strLanguage_Typebox(34) '+Infinity
                    End If
                End If
            End If

        ElseIf TypeBoxMode = TypeMode._Decimal Then
            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
            txtInput.Visible = True
            txtInput.Focus()

            If MinimumValidDecimal <> Decimal.MinValue OrElse MaximumValidDecimal <> Decimal.MinValue Then
                lblValidRange.Visible = True
            End If
            lblValidRange.Text = strLanguage_Typebox(31)    'Valid Range:
            If MinimumValidDecimal <> Decimal.MinValue Then
                lblValidRange.Text &= "[ " & MinimumValidDecimal & " , "
                strLowerLimit = CStr(MinimumValidDecimal)
            Else
                lblValidRange.Text &= "[ " & strLanguage_Typebox(33) & " , " '-Infinity
                strLowerLimit = strLanguage_Typebox(33)
            End If
            If MaximumValidDecimal <> Decimal.MinValue Then
                lblValidRange.Text &= MaximumValidDecimal & " ]"
                strUpperLimit = CStr(MaximumValidDecimal)
            Else
                lblValidRange.Text &= strLanguage_Typebox(34) & " ]" '+Infinity
                strUpperLimit = strLanguage_Typebox(34)
            End If

        ElseIf TypeBoxMode = TypeMode._Double Then
            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
            txtInput.Visible = True
            txtInput.Focus()

            If MinimumValidDouble <> Double.MinValue OrElse MaximumValidDouble <> Double.MinValue Then
                lblValidRange.Visible = True
            End If
            lblValidRange.Text = strLanguage_Typebox(31)    'Valid Range:
            If MinimumValidDouble <> Double.MinValue Then
                lblValidRange.Text &= "[ " & MinimumValidDouble & " , "
                strLowerLimit = CStr(MinimumValidDouble)
            Else
                lblValidRange.Text &= "[ " & strLanguage_Typebox(33) & " , " '-Infinity
                strLowerLimit = strLanguage_Typebox(33)
            End If
            If MaximumValidDouble <> Double.MinValue Then
                lblValidRange.Text &= MaximumValidDouble & " ]"
                strUpperLimit = CStr(MaximumValidDouble)
            Else
                lblValidRange.Text &= strLanguage_Typebox(34) & " ]" '+Infinity
                strUpperLimit = strLanguage_Typebox(34)
            End If

        ElseIf TypeBoxMode = TypeMode._Single Then
            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
            txtInput.Visible = True
            txtInput.Focus()

            If MinimumValidSingle <> Single.MinValue OrElse MaximumValidSingle <> Single.MinValue Then
                lblValidRange.Visible = True
            End If
            lblValidRange.Text = strLanguage_Typebox(31)    'Valid Range:
            If MinimumValidSingle <> Single.MinValue Then
                lblValidRange.Text &= "[ " & MinimumValidSingle & " , "
                strLowerLimit = CStr(MinimumValidSingle)
            Else
                lblValidRange.Text &= "[ " & strLanguage_Typebox(33) & " , " '-Infinity
                strLowerLimit = strLanguage_Typebox(33)
            End If
            If MaximumValidSingle <> Single.MinValue Then
                lblValidRange.Text &= MaximumValidSingle & " ]"
                strUpperLimit = CStr(MaximumValidSingle)
            Else
                lblValidRange.Text &= strLanguage_Typebox(34) & " ]" '+Infinity
                strUpperLimit = strLanguage_Typebox(34)
            End If

        ElseIf TypeBoxMode = TypeMode._Integer Then
            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
            txtInput.Visible = True
            txtInput.Focus()

            If MinimumValidInteger <> Integer.MinValue OrElse MaximumValidInteger <> Integer.MinValue Then
                lblValidRange.Visible = True
            End If
            lblValidRange.Text = strLanguage_Typebox(31)    'Valid Range:
            If MinimumValidInteger <> Integer.MinValue Then
                lblValidRange.Text &= "[ " & MinimumValidInteger & " , "
                strLowerLimit = CStr(MinimumValidInteger)
            Else
                lblValidRange.Text &= "[ " & strLanguage_Typebox(33) & " , " '-Infinity
                strLowerLimit = strLanguage_Typebox(33)
            End If
            If MaximumValidInteger <> Integer.MinValue Then
                lblValidRange.Text &= MaximumValidInteger & " ]"
                strUpperLimit = CStr(MaximumValidInteger)
            Else
                lblValidRange.Text &= strLanguage_Typebox(34) & " ]" '+Infinity
                strUpperLimit = strLanguage_Typebox(34)
            End If

        ElseIf TypeBoxMode = TypeMode._UInteger Then
            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
            txtInput.Visible = True
            txtInput.Focus()

            If MinimumValidUInteger <> UInteger.MaxValue OrElse MaximumValidUInteger <> UInteger.MaxValue Then
                lblValidRange.Visible = True
            End If
            lblValidRange.Text = strLanguage_Typebox(31)    'Valid Range:
            If MinimumValidUInteger <> UInteger.MaxValue Then
                lblValidRange.Text &= "[ " & MinimumValidUInteger & " , "
                strLowerLimit = CStr(MinimumValidUInteger)
            Else
                lblValidRange.Text &= "[ 0 , "
                strLowerLimit = "0"
            End If
            If MaximumValidUInteger <> UInteger.MaxValue Then
                lblValidRange.Text &= MaximumValidUInteger & " ]"
                strUpperLimit = CStr(MaximumValidUInteger)
            Else
                lblValidRange.Text &= strLanguage_Typebox(34) & " ]" '+Infinity
                strUpperLimit = strLanguage_Typebox(34)
            End If

        ElseIf TypeBoxMode = TypeMode._Short Then
            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
            txtInput.Visible = True
            txtInput.Focus()

            If MinimumValidShort <> Short.MinValue OrElse MaximumValidShort <> Short.MinValue Then
                lblValidRange.Visible = True
            End If
            lblValidRange.Text = strLanguage_Typebox(31)    'Valid Range:
            If MinimumValidShort <> Short.MinValue Then
                lblValidRange.Text &= "[ " & MinimumValidShort & " , "
                strLowerLimit = CStr(MinimumValidShort)
            Else
                lblValidRange.Text &= "[ " & strLanguage_Typebox(33) & " , " '-Infinity
                strLowerLimit = strLanguage_Typebox(33)
            End If
            If MaximumValidShort <> Short.MinValue Then
                lblValidRange.Text &= MaximumValidShort & " ]"
                strUpperLimit = CStr(MaximumValidShort)
            Else
                lblValidRange.Text &= strLanguage_Typebox(34) & " ]" '+Infinity
                strUpperLimit = strLanguage_Typebox(34)
            End If

        ElseIf TypeBoxMode = TypeMode._UShort Then
            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
            txtInput.Visible = True
            txtInput.Focus()

            If MinimumValidUShort <> UShort.MaxValue OrElse MaximumValidUShort <> UShort.MaxValue Then
                lblValidRange.Visible = True
            End If
            lblValidRange.Text = strLanguage_Typebox(31)    'Valid Range:
            If MinimumValidUShort <> UShort.MaxValue Then
                lblValidRange.Text &= "[ " & MinimumValidUShort & " , "
                strLowerLimit = CStr(MinimumValidUShort)
            Else
                lblValidRange.Text &= "[ 0 , "
                strLowerLimit = "0"
            End If
            If MaximumValidUShort <> UShort.MaxValue Then
                lblValidRange.Text &= MaximumValidUShort & " ]"
                strUpperLimit = CStr(MaximumValidUShort)
            Else
                lblValidRange.Text &= strLanguage_Typebox(34) & " ]" '+Infinity
                strUpperLimit = strLanguage_Typebox(34)
            End If

        ElseIf TypeBoxMode = TypeMode._Long Then
            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
            txtInput.Visible = True
            txtInput.Focus()

            If MinimumValidLong <> Long.MinValue OrElse MaximumValidLong <> Long.MinValue Then
                lblValidRange.Visible = True
            End If
            lblValidRange.Text = strLanguage_Typebox(31)    'Valid Range:
            If MinimumValidLong <> Long.MinValue Then
                lblValidRange.Text &= "[ " & MinimumValidLong & " , "
                strLowerLimit = CStr(MinimumValidLong)
            Else
                lblValidRange.Text &= "[ " & strLanguage_Typebox(33) & " , " '-Infinity
                strLowerLimit = strLanguage_Typebox(33)
            End If
            If MaximumValidLong <> Long.MinValue Then
                lblValidRange.Text &= MaximumValidLong & " ]"
                strUpperLimit = CStr(MaximumValidLong)
            Else
                lblValidRange.Text &= strLanguage_Typebox(34) & " ]" '+Infinity
                strUpperLimit = strLanguage_Typebox(34)
            End If

        ElseIf TypeBoxMode = TypeMode._ULong Then
            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
            txtInput.Visible = True
            txtInput.Focus()

            If MinimumValidULong <> ULong.MaxValue OrElse MaximumValidULong <> ULong.MaxValue Then
                lblValidRange.Visible = True
            End If
            lblValidRange.Text = strLanguage_Typebox(31)    'Valid Range:
            If MinimumValidULong <> ULong.MaxValue Then
                lblValidRange.Text &= "[ " & MinimumValidULong & " , "
                strLowerLimit = CStr(MinimumValidULong)
            Else
                lblValidRange.Text &= "[ 0 , "
                strLowerLimit = "0"
            End If
            If MaximumValidULong <> ULong.MaxValue Then
                lblValidRange.Text &= MaximumValidULong & " ]"
                strUpperLimit = CStr(MaximumValidULong)
            Else
                lblValidRange.Text &= strLanguage_Typebox(34) & " ]" '+Infinity
                strUpperLimit = strLanguage_Typebox(34)
            End If

        ElseIf TypeBoxMode = TypeMode._Date Then
            If ValidDateFrom <> DefaultDate Or ValidDateTo <> DefaultDate Then
                lblValidRange.Visible = True
                lblValidRange.Text = strLanguage_Typebox(31)    'Valid Range:
                If ValidDateFrom <> DefaultDate Then
                    lblValidRange.Text &= "[ " & ValidDateFrom & ", "
                    dtFrom.Value = ValidDateFrom
                Else
                    lblValidRange.Text &= "[ " & strLanguage_Typebox(32) & ", "  'Any Date
                    dtFrom.Value = DefaultDate
                End If
                If ValidDateTo <> DefaultDate Then
                    lblValidRange.Text &= ValidDateTo & " ]"
                    dtTo.Value = ValidDateTo
                Else
                    lblValidRange.Text &= strLanguage_Typebox(32) & " ]"  'Any Date
                    dtTo.Value = DefaultDate
                End If
            End If

            If DateFormat = "" Then DateFormat = DefaultDateTimeFormat

            If MultiLine = True Then
                txtInput.Visible = True
                txtInput.Multiline = True

            Else
                dtFrom.CustomFormat = DateFormat
                dtTo.CustomFormat = DateFormat

                dtFrom.Visible = True
                If isRange Then
                    dtTo.Visible = True
                    If ValidDateTo <> DefaultDate Then
                        dtTo.Value = ValidDateTo
                    Else
                        dtTo.Value = DefaultDate
                    End If
                Else
                    dtFrom.Size = New Size((dtFrom.Size.Width * 2) + 6, dtFrom.Size.Height)
                End If
                dtFrom.Focus()

            End If

        ElseIf TypeBoxMode = TypeMode._ComboBox Then
            cbIndexReturner.Visible = True
            cbIndexReturner.DataSource = ComboBoxDataSource
            cbIndexReturner.Focus()

        Else
            MsgBox(strLanguage_Typebox(11), MsgBoxStyle.Critical) 'Unknown procedure on Typebox
            DialogResult = DialogResult.Ignore
        End If

    End Sub

#Region "Text"
    Private Sub subText_String_CanStartWithNum()
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub subText_String_CANTStartWithNum()
        If IsNumeric(strUserInput(0)) Then
            MsgBox(strLanguage_Typebox(8) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Text must not start with a number
            DialogResult = DialogResult.None
        Else
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub subText_Array_CanStartWithNum()
        Dim strInputLines() As String = strUserInputs
        Dim EverythingIsInsideTheValidBounds As Boolean = True

        If doEraseNullLines Then strInputLines = EraseNullLines(strInputLines)

        For i = 0 To strInputLines.Length - 1
            If MinimumValidTextSizePerLine <> -1 AndAlso MinimumValidTextSizePerLine <> Double.MinValue AndAlso strInputLines(i).Length < MinimumValidTextSizePerLine Then
                MsgBox(strLanguage_Typebox(21) & MinimumValidTextSizePerLine & vbCrLf & strLanguage_Typebox(22) & (i + 1) & strLanguage_Typebox(39), MsgBoxStyle.Exclamation) 'The minimum allowed length per line is:
                DialogResult = DialogResult.None
                EverythingIsInsideTheValidBounds = False
                Exit For
            ElseIf MaximumValidTextSizePerLine <> -1 AndAlso MaximumValidTextSizePerLine <> Double.MinValue AndAlso strInputLines(i).Length > MaximumValidTextSizePerLine Then
                MsgBox(strLanguage_Typebox(23) & MaximumValidTextSizePerLine & strLanguage_Typebox(24) & vbCrLf & strLanguage_Typebox(25) & (i + 1) & strLanguage_Typebox(39), MsgBoxStyle.Exclamation) 'You may type up to x characters on each line.
                DialogResult = DialogResult.None
                EverythingIsInsideTheValidBounds = False
                Exit For
            End If
        Next

        If EverythingIsInsideTheValidBounds Then
            txtInput.Lines = strInputLines
            DialogResult = DialogResult.OK
            Close()
        End If

    End Sub

    Private Sub subText_Array_CANTStartWithNum()
        Dim strInputLines() As String = strUserInputs
        Dim isNot1stCharNum As Boolean = True

        If doEraseNullLines Then strInputLines = EraseNullLines(strInputLines)

        For i = 0 To strInputLines.Length - 1
            If IsNumeric(strInputLines(i)(0)) Then
                isNot1stCharNum = False
                Exit For
            Else
                If MinimumValidTextSizePerLine <> -1 AndAlso MinimumValidTextSizePerLine <> Double.MinValue AndAlso strInputLines(i).Length < MinimumValidTextSizePerLine Then
                    MsgBox(strLanguage_Typebox(21) & MinimumValidTextSizePerLine & vbCrLf & strLanguage_Typebox(22) & (i + 1) & strLanguage_Typebox(39), MsgBoxStyle.Exclamation) 'The minimum allowed length per line is:
                    DialogResult = DialogResult.None
                    Exit Sub 'So we don't proceed to "1st Char Num" check
                Else
                    If MaximumValidTextSizePerLine <> -1 AndAlso MaximumValidTextSizePerLine <> Double.MinValue AndAlso strInputLines(i).Length > MaximumValidTextSizePerLine Then
                        MsgBox(strLanguage_Typebox(23) & MaximumValidTextSizePerLine & strLanguage_Typebox(24) & vbCrLf & strLanguage_Typebox(25) & (i + 1) & strLanguage_Typebox(39), MsgBoxStyle.Exclamation) 'You may type up to x characters on each line.
                        DialogResult = DialogResult.None
                        Exit Sub 'So we don't proceed to "1st Char Num" check
                    End If
                End If
            End If
        Next

        If isNot1stCharNum Then
            txtInput.Lines = strInputLines
            DialogResult = DialogResult.OK
            Close()

        Else
            MsgBox(strLanguage_Typebox(27) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'No line may begin with a numeric value
            DialogResult = DialogResult.None
        End If
    End Sub

#End Region

#Region "StrDouble"
    Private Sub subStrDouble_Value()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            ReturnedStrDoubleOne = strUserInput
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strNumber As String = SimplifyValue(strUserInput).ToString '   Lets try to transform the text into a number

            '                               If it zero by rounding, then result will be non-numeric text on StatRound! Hence, extra code to check the bounds by "0" and not "<0.x"
            If RoundNum <> -1 Then strNumber = StatRound(strUserInput, CUInt(RoundNum), MinNumLength, AtLeastOneBecameZeroByRounding) '    Rounding the Number if we must
            Dim tmpNum As String
            If AtLeastOneBecameZeroByRounding Then tmpNum = "0" Else tmpNum = strNumber

            If Not IsNumeric(tmpNum) Then '                      If it failed, lets warn the user
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation) ' Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None

                '                                                   If the number is outside the bounds, lets warn the user
            ElseIf (MinimumValidDouble <> Double.MinValue AndAlso CDbl(tmpNum) < MinimumValidDouble) OrElse (MaximumValidDouble <> Double.MinValue AndAlso CDbl(tmpNum) > MaximumValidDouble) Then
                MsgBox(strLanguage_Typebox(26) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]") 'The number you typed is outside the bounds of the valid range:
                DialogResult = DialogResult.None

                '                                                   Else, everything's ok
            Else
                strUserInput = strNumber
                DialogResult = DialogResult.OK
                Close()
            End If

        End If
    End Sub

    Private Sub subStrDouble_Range()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedStrDoubleOne = ""
            ReturnedStrDoubleTwo = ""
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim Delimiter As String = DelimiterFinder(strUserInput)
            If Delimiter <> String.Empty Then
                Dim strDoubleRange() As String = Split(strUserInput, Delimiter)

                If strDoubleRange.Length <> 2 Then
                    MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation) 'This is not a valid numeric range format.
                    txtInput.Text = ""
                    DialogResult = DialogResult.None

                Else
                    Dim isTheRangeComprisedOfOnlyNums As Boolean '                                                      This is separate so as not to confuse the mind with the "Array" word of the func
                    Dim strDoubleRangeNew() As String = SimplifyArray(strDoubleRange, isTheRangeComprisedOfOnlyNums) '  Lets try to transform the text into a numbers

                    If isTheRangeComprisedOfOnlyNums Then
                        Dim BecameZeroByRounding() As Boolean = {} '                                                                                    If even one becomes zero by rounding, then result will be non-numeric text on StatRound! Hence, extra code to check the bounds by "0" and not "<0.x"
                        If RoundNum <> -1 Then strDoubleRangeNew = StatRound(strDoubleRangeNew, CUInt(RoundNum), MinNumLength, BecameZeroByRounding, AtLeastOneBecameZeroByRounding) '  Rounding the Number if we must

                        Dim tmpNums As List(Of String) = strDoubleRangeNew.ToList '                                                                     Extra code ensuring that bounds check be possible
                        For i As Integer = 0 To BecameZeroByRounding.Length - 1
                            If BecameZeroByRounding(i) = True Then tmpNums.Item(i) = "0"
                        Next

                        'If the number is outside the bounds, lets warn the user                                                                                  3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                        If MinimumValidDouble <> Double.MinValue AndAlso IsNumeric(tmpNums(0)) AndAlso IsNumeric(tmpNums(1)) AndAlso ((CDbl(tmpNums(0)) <= CDbl(tmpNums(1)) AndAlso CDbl(tmpNums(0)) < MinimumValidDouble) OrElse (CDbl(tmpNums(0)) > CDbl(tmpNums(1)) AndAlso CDbl(tmpNums(1)) < MinimumValidDouble)) Then
                            MsgBox(strLanguage_Typebox(28) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                            DialogResult = DialogResult.None

                            'If the number is outside the bounds, lets warn the user                                                                                  3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                        ElseIf MaximumValidDouble <> Double.MinValue AndAlso IsNumeric(tmpNums(0)) AndAlso IsNumeric(tmpNums(1)) AndAlso ((CDbl(tmpNums(0)) <= CDbl(tmpNums(1)) AndAlso CDbl(tmpNums(1)) > MaximumValidDouble) OrElse (CDbl(tmpNums(0)) > CDbl(tmpNums(1)) AndAlso CDbl(tmpNums(0)) > MaximumValidDouble)) Then
                            MsgBox(strLanguage_Typebox(29) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                            DialogResult = DialogResult.None

                        Else
                            ReturnedStrDoubleOne = strDoubleRangeNew(0)
                            ReturnedStrDoubleTwo = strDoubleRangeNew(1)
                            DialogResult = DialogResult.OK
                            Close()

                        End If

                    Else
                        MsgBox(strLanguage_Typebox(18), MsgBoxStyle.Exclamation) 'You must only type numbers, no letters are allowed.
                        DialogResult = DialogResult.None
                    End If

                End If

            Else
                MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation) 'This is not a valid numeric range format.
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subStrDouble_ValueArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedStrDoubleArrayOne = Nothing
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = True

            Dim strNumbers() As String = SimplifyArray(strInputLines, isOverallNumeric)

            If isOverallNumeric = True Then
                Dim BecameZeroByRounding() As Boolean = {} '                                                                        If even one becomes zero by rounding, then result will be non-numeric text on StatRound! Hence, extra code to check the bounds by "0" and not "<0.x"
                If RoundNum <> -1 Then strNumbers = StatRound(strNumbers, CUInt(RoundNum), MinNumLength, BecameZeroByRounding, AtLeastOneBecameZeroByRounding) 'Rounding the Number if we must

                Dim tmpNums As List(Of String) = strNumbers.ToList '                                                                     Extra code ensuring that bounds check be possible
                For i As Integer = 0 To BecameZeroByRounding.Length - 1
                    If BecameZeroByRounding(i) = True Then tmpNums.Item(i) = "0"
                Next

                For i = 0 To strNumbers.Length - 1
                    If (MinimumValidDouble <> Double.MinValue AndAlso CDbl(tmpNums(i)) < MinimumValidDouble) OrElse (MaximumValidDouble <> Double.MinValue AndAlso CDbl(tmpNums(i)) > MaximumValidDouble) Then
                        MsgBox(strLanguage_Typebox(40).Replace("{Line}", (i + 1).ToString).Replace("{Num}", tmpNums(i)) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]", MsgBoxStyle.Exclamation) 'The number you typed is outside the bounds of the valid range:
                        ForcedExitSub = True
                        DialogResult = DialogResult.None
                        Exit Sub 'To Avoid the Overall Numeric Check

                    Else
                        ReDim Preserve ReturnedStrDoubleArrayOne(i)
                        ReturnedStrDoubleArrayOne(i) = strNumbers(i)
                        DialogResult = DialogResult.None
                    End If
                Next

                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subStrDouble_RangeArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedStrDoubleArrayOne = Nothing
            ReturnedStrDoubleArrayTwo = Nothing
            txtInput.Text = CStr(Double.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = False

            Dim strDoubleRange()() As String = New String(strInputLines.Length - 1)() {}
            ReturnedStrDoubleArrayOne = Nothing
            ReturnedStrDoubleArrayTwo = Nothing

            For j = 0 To strInputLines.Length - 1
                Dim Delimiter As String = DelimiterFinder(strInputLines(j))
                If Delimiter <> String.Empty Then
                    strDoubleRange(j) = Split(strInputLines(j), Delimiter)

                    If strDoubleRange(j).Length = 2 Then
                        Dim strDoubleRangeNew() = SimplifyArray(strDoubleRange(j), isOverallNumeric)

                        If Not isOverallNumeric Then
                            ForcedExitSub = True
                            DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                            Exit For

                        Else
                            Dim BecameZeroByRounding() As Boolean = {} '                                                                        If even one becomes zero by rounding, then result will be non-numeric text on StatRound! Hence, extra code to check the bounds by "0" and not "<0.x"
                            If RoundNum <> -1 Then strDoubleRangeNew = StatRound(strDoubleRangeNew, CUInt(RoundNum), MinNumLength, BecameZeroByRounding, AtLeastOneBecameZeroByRounding) 'Rounding the Number if we must

                            Dim tmpNums As List(Of String) = strDoubleRangeNew.ToList '                                                                     Extra code ensuring that bounds check be possible
                            For i As Integer = 0 To BecameZeroByRounding.Length - 1
                                If BecameZeroByRounding(i) = True Then tmpNums.Item(i) = "0"
                            Next

                            'If the number is outside the bounds, lets warn the user                                                                                  3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                            If MinimumValidDouble <> Double.MinValue AndAlso IsNumeric(tmpNums(0)) AndAlso IsNumeric(tmpNums(1)) AndAlso ((CDbl(tmpNums(0)) <= CDbl(tmpNums(1)) AndAlso CDbl(tmpNums(0)) < MinimumValidDouble) OrElse (CDbl(tmpNums(0)) > CDbl(tmpNums(1)) AndAlso CDbl(tmpNums(1)) < MinimumValidDouble)) Then
                                MsgBox(strLanguage_Typebox(41).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In tmpNums Order By CDbl(Num) Ascending).FirstOrDefault) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                DialogResult = DialogResult.None
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                'If the number is outside the bounds, lets warn the user                                                                                  3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                            ElseIf MaximumValidDouble <> Double.MinValue AndAlso IsNumeric(tmpNums(0)) AndAlso IsNumeric(tmpNums(1)) AndAlso ((CDbl(tmpNums(0)) <= CDbl(tmpNums(1)) AndAlso CDbl(tmpNums(1)) > MaximumValidDouble) OrElse (CDbl(tmpNums(0)) > CDbl(tmpNums(1)) AndAlso CDbl(tmpNums(0)) > MaximumValidDouble)) Then
                                MsgBox(strLanguage_Typebox(42).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In tmpNums Order By CDbl(Num) Descending).FirstOrDefault) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                            Else 'If everything is right:
                                ReDim Preserve ReturnedStrDoubleArrayOne(j)
                                ReturnedStrDoubleArrayOne(j) = strDoubleRangeNew(0)
                                ReDim Preserve ReturnedStrDoubleArrayTwo(j)
                                ReturnedStrDoubleArrayTwo(j) = strDoubleRangeNew(1)
                            End If

                        End If


                    Else
                        MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        ForcedExitSub = True
                        DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                        Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP
                    End If

                Else
                    MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    ForcedExitSub = True
                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                    Exit Sub 'To avoid the Overall Numeric Check AND the LOOP
                End If
            Next

            If isOverallNumeric Then
                'Text's Lines have been found all valid number-ranges, now the whole lines() will be returned
                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
            End If
        End If
    End Sub
#End Region

#Region "Double"
    Private Sub subDouble_Value()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            txtInput.Text = CStr(Double.MinValue)
            ReturnedDoubleOne = Double.MinValue
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strNumber As String = SimplifyValue(strUserInput).ToString '   Lets try to transform the text into a number
            If RoundNum <> -1 Then strNumber = NormalRound(strUserInput, CUInt(RoundNum), MinNumLength, AtLeastOneBecameZeroByRounding) '    Rounding the Number if we must

            If Not IsNumeric(strNumber) Then '                      If it failed, lets warn the user
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation) 'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None

                '                                                   If the number is outside the bounds, lets warn the user
            ElseIf (MinimumValidDouble <> Double.MinValue AndAlso CDbl(strNumber) < MinimumValidDouble) OrElse (MaximumValidDouble <> Double.MinValue AndAlso CDbl(strNumber) > MaximumValidDouble) Then
                MsgBox(strLanguage_Typebox(26) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]") 'The number you typed is outside the bounds of the valid range:
                DialogResult = DialogResult.None

                '                                                   Else, everything's ok
            Else
                ReturnedDoubleOne = CDbl(strNumber)
                DialogResult = DialogResult.OK
                Close()
            End If
        End If
    End Sub

    Private Sub subDouble_Range()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            ReturnedDoubleOne = Double.MinValue
            ReturnedDoubleTwo = Double.MinValue
            txtInput.Text = CStr(Double.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim Delimiter As String = DelimiterFinder(strUserInput)
            If Delimiter <> String.Empty Then
                Dim strDoubleRange() As String = Split(strUserInput, Delimiter)

                If strDoubleRange.Length <> 2 Then 'If it isn't a real range, lets warn the user
                    MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    txtInput.Text = ""
                    DialogResult = DialogResult.None

                Else
                    Dim isTheRangeComprisedOfOnlyNums As Boolean 'This is separate so as not to confuse the mind with the "Array" word of the func
                    Dim strDoubleRangeNew() As String = SimplifyArray(strDoubleRange, isTheRangeComprisedOfOnlyNums) '  Lets try to transform the text into numbers

                    If isTheRangeComprisedOfOnlyNums Then
                        If RoundNum <> -1 Then strDoubleRangeNew = NormalRound(strDoubleRangeNew, CUInt(RoundNum), MinNumLength, , AtLeastOneBecameZeroByRounding) '  Rounding the Number if we must

                        'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                        If MinimumValidDouble <> Double.MinValue AndAlso ((CDbl(strDoubleRangeNew(0)) <= CDbl(strDoubleRangeNew(1)) AndAlso CDbl(strDoubleRangeNew(0)) < MinimumValidDouble) OrElse (CDbl(strDoubleRangeNew(0)) > CDbl(strDoubleRangeNew(1)) AndAlso CDbl(strDoubleRangeNew(1)) < MinimumValidDouble)) Then
                            MsgBox(strLanguage_Typebox(28) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                            DialogResult = DialogResult.None

                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                        ElseIf MaximumValidDouble <> Double.MinValue AndAlso ((CDbl(strDoubleRangeNew(0)) <= CDbl(strDoubleRangeNew(1)) AndAlso CDbl(strDoubleRangeNew(1)) > MaximumValidDouble) OrElse (CDbl(strDoubleRangeNew(0)) > CDbl(strDoubleRangeNew(1)) AndAlso CDbl(strDoubleRangeNew(0)) > MaximumValidDouble)) Then
                            MsgBox(strLanguage_Typebox(29) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                            DialogResult = DialogResult.None

                            '                                                   Else, everything's ok
                        Else
                            ReturnedDoubleOne = CDbl(strDoubleRangeNew(0))
                            ReturnedDoubleTwo = CDbl(strDoubleRangeNew(1))
                            DialogResult = DialogResult.OK
                            Close()

                        End If

                    Else
                        MsgBox(strLanguage_Typebox(18), MsgBoxStyle.Exclamation) 'You must only type numbers, no letters are allowed.
                        DialogResult = DialogResult.None
                    End If

                End If

            Else
                MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subDouble_ValueArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            ReturnedDoubleArrayOne = Nothing
            txtInput.Text = CStr(Double.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = True

            Dim strNumbers() As String = SimplifyArray(strInputLines, isOverallNumeric) '  Lets try to transform the text into numbers

            If isOverallNumeric = True Then
                If RoundNum <> -1 Then strNumbers = NormalRound(strNumbers, CUInt(RoundNum), MinNumLength, , AtLeastOneBecameZeroByRounding) 'Rounding the Number if we must

                For i = 0 To strNumbers.Length - 1
                    '                                                       If the number is outside the bounds, lets warn the user
                    If (MinimumValidDouble <> Double.MinValue AndAlso CDbl(strNumbers(i)) < MinimumValidDouble) OrElse (MaximumValidDouble <> Double.MinValue AndAlso CDbl(strNumbers(i)) > MaximumValidDouble) Then
                        MsgBox(strLanguage_Typebox(40).Replace("{Line}", (i + 1).ToString).Replace("{Num}", strNumbers(i)) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]", MsgBoxStyle.Exclamation) 'The number you typed is outside the bounds of the valid range:
                        ForcedExitSub = True
                        DialogResult = DialogResult.None
                        Exit Sub 'To Avoid the Overall Numeric Check

                        '                                                   Else, everything's ok
                    Else
                        ReDim Preserve ReturnedDoubleArrayOne(i)
                        ReturnedDoubleArrayOne(i) = CDbl(strNumbers(i))
                        DialogResult = DialogResult.None
                    End If
                Next

                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subDouble_RangeArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            ReturnedDoubleArrayOne = Nothing
            ReturnedDoubleArrayTwo = Nothing
            txtInput.Text = CStr(Double.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = False

            Dim strDoubleRange()() As String = New String(strInputLines.Length - 1)() {}
            ReturnedDoubleArrayOne = Nothing
            ReturnedDoubleArrayTwo = Nothing

            For j = 0 To strInputLines.Length - 1
                Dim Delimiter As String = DelimiterFinder(strInputLines(j))
                If Delimiter <> String.Empty Then
                    strDoubleRange(j) = Split(strInputLines(j), Delimiter)

                    If strDoubleRange(j).Length = 2 Then
                        Dim strDoubleRangeNew() = SimplifyArray(strDoubleRange(j), isOverallNumeric) '  Lets try to transform the text into numbers

                        If Not isOverallNumeric Then
                            ForcedExitSub = True
                            DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                            Exit For

                        Else
                            If RoundNum <> -1 Then strDoubleRangeNew = NormalRound(strDoubleRangeNew, CUInt(RoundNum), MinNumLength, , AtLeastOneBecameZeroByRounding) 'Rounding the Number if we must

                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                            If MinimumValidDouble <> Double.MinValue AndAlso ((CDbl(strDoubleRangeNew(0)) <= CDbl(strDoubleRangeNew(1)) AndAlso CDbl(strDoubleRangeNew(0)) < MinimumValidDouble) OrElse (CDbl(strDoubleRangeNew(0)) > CDbl(strDoubleRangeNew(1)) AndAlso CDbl(strDoubleRangeNew(1)) < MinimumValidDouble)) Then
                                MsgBox(strLanguage_Typebox(41).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strDoubleRangeNew Order By CDbl(Num) Ascending).FirstOrDefault) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                DialogResult = DialogResult.None
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                            ElseIf MaximumValidDouble <> Double.MinValue AndAlso ((CDbl(strDoubleRangeNew(0)) <= CDbl(strDoubleRangeNew(1)) AndAlso CDbl(strDoubleRangeNew(1)) > MaximumValidDouble) OrElse (CDbl(strDoubleRangeNew(0)) > CDbl(strDoubleRangeNew(1)) AndAlso CDbl(strDoubleRangeNew(0)) > MaximumValidDouble)) Then
                                MsgBox(strLanguage_Typebox(42).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strDoubleRangeNew Order By CDbl(Num) Descending).FirstOrDefault) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                            Else 'If everything is right:
                                ReDim Preserve ReturnedDoubleArrayOne(j)
                                ReturnedDoubleArrayOne(j) = CDbl(strDoubleRangeNew(0))
                                ReDim Preserve ReturnedDoubleArrayTwo(j)
                                ReturnedDoubleArrayTwo(j) = CDbl(strDoubleRangeNew(1))
                            End If

                        End If


                    Else
                        MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        ForcedExitSub = True
                        DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                        Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP
                    End If

                Else
                    MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    ForcedExitSub = True
                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                    Exit Sub 'To avoid the Overall Numeric Check AND the LOOP
                End If
            Next

            If isOverallNumeric Then
                'Text's Lines have been found all valid number-ranges, now the whole lines() will be returned
                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
            End If
        End If
    End Sub
#End Region

#Region "Decimal"
    Private Sub subDecimal_Value()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            txtInput.Text = CStr(Decimal.MinValue)
            ReturnedDecimalOne = Decimal.MinValue
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strNumber As String = SimplifyValue(strUserInput).ToString '   Lets try to transform the text into a number

            If RoundNum <> -1 Then strNumber = NormalRound(strUserInput, CUInt(RoundNum), MinNumLength, AtLeastOneBecameZeroByRounding) '    Rounding the Number if we must

            If Not IsNumeric(strNumber) Then '                      If it failed, lets warn the user
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation) 'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None

                '                                                   If the number is outside the bounds, lets warn the user
            ElseIf (MinimumValidDecimal <> Decimal.MinValue AndAlso CDec(strNumber.Replace(".", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)) < MinimumValidDecimal) OrElse (MaximumValidDecimal <> Decimal.MinValue AndAlso CDec(strNumber.Replace(".", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)) > MaximumValidDecimal) Then
                MsgBox(strLanguage_Typebox(26) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]") 'The number you typed is outside the bounds of the valid range:
                DialogResult = DialogResult.None

                '                                                   Else, everything's ok
            Else
                ReturnedDecimalOne = CDec(strNumber.Replace(".", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                DialogResult = DialogResult.OK
                Close()
            End If
        End If
    End Sub

    Private Sub subDecimal_Range()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            ReturnedDecimalOne = Decimal.MinValue
            ReturnedDecimalTwo = Decimal.MinValue
            txtInput.Text = CStr(Decimal.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim Delimiter As String = DelimiterFinder(strUserInput)
            If Delimiter <> String.Empty Then
                Dim strDecimalRange() As String = Split(strUserInput, Delimiter)

                If strDecimalRange.Length <> 2 Then 'If it isn't a real range, lets warn the user
                    MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    txtInput.Text = ""
                    DialogResult = DialogResult.None

                Else
                    Dim isTheRangeComprisedOfOnlyNums As Boolean 'This is separate so as not to confuse the mind with the "Array" word of the func
                    Dim strDecimalRangeNew() As String = SimplifyArray(strDecimalRange, isTheRangeComprisedOfOnlyNums) '  Lets try to transform the text into numbers

                    If isTheRangeComprisedOfOnlyNums Then
                        If RoundNum <> -1 Then strDecimalRangeNew = NormalRound(strDecimalRangeNew, CUInt(RoundNum), MinNumLength, , AtLeastOneBecameZeroByRounding) '  Rounding the Number if we must

                        'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                        If MinimumValidDecimal <> Decimal.MinValue AndAlso ((CDec(strDecimalRangeNew(0)) <= CDec(strDecimalRangeNew(1)) AndAlso CDec(strDecimalRangeNew(0)) < MinimumValidDecimal) OrElse (CDec(strDecimalRangeNew(0)) > CDec(strDecimalRangeNew(1)) AndAlso CDec(strDecimalRangeNew(1)) < MinimumValidDecimal)) Then
                            MsgBox(strLanguage_Typebox(28) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                            DialogResult = DialogResult.None

                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                        ElseIf MaximumValidDecimal <> Decimal.MinValue AndAlso ((CDec(strDecimalRangeNew(0)) <= CDec(strDecimalRangeNew(1)) AndAlso CDec(strDecimalRangeNew(1)) > MaximumValidDecimal) OrElse (CDec(strDecimalRangeNew(0)) > CDec(strDecimalRangeNew(1)) AndAlso CDec(strDecimalRangeNew(0)) > MaximumValidDecimal)) Then
                            MsgBox(strLanguage_Typebox(29) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                            DialogResult = DialogResult.None

                            '                                                   Else, everything's ok
                        Else
                            ReturnedDecimalOne = CDec(strDecimalRangeNew(0))
                            ReturnedDecimalTwo = CDec(strDecimalRangeNew(1))
                            DialogResult = DialogResult.OK
                            Close()

                        End If

                    Else
                        MsgBox(strLanguage_Typebox(18), MsgBoxStyle.Exclamation) 'You must only type numbers, no letters are allowed.
                        DialogResult = DialogResult.None
                    End If

                End If

            Else
                MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subDecimal_ValueArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            ReturnedDecimalArrayOne = Nothing
            txtInput.Text = CStr(Decimal.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = True

            Dim strNumbers() As String = SimplifyArray(strInputLines, isOverallNumeric) '  Lets try to transform the text into numbers

            If isOverallNumeric = True Then
                If RoundNum <> -1 Then strNumbers = NormalRound(strNumbers, CUInt(RoundNum), MinNumLength, , AtLeastOneBecameZeroByRounding) 'Rounding the Number if we must

                For i = 0 To strNumbers.Length - 1
                    '                                                       If the number is outside the bounds, lets warn the user
                    If (MinimumValidDecimal <> Decimal.MinValue AndAlso CDec(strNumbers(i)) < MinimumValidDecimal) OrElse (MaximumValidDecimal <> Decimal.MinValue AndAlso CDec(strNumbers(i)) > MaximumValidDecimal) Then
                        MsgBox(strLanguage_Typebox(40).Replace("{Line}", (i + 1).ToString).Replace("{Num}", strNumbers(i)) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]", MsgBoxStyle.Exclamation) 'The number you typed is outside the bounds of the valid range:
                        ForcedExitSub = True
                        DialogResult = DialogResult.None
                        Exit Sub 'To Avoid the Overall Numeric Check

                        '                                                   Else, everything's ok
                    Else
                        ReDim Preserve ReturnedDecimalArrayOne(i)
                        ReturnedDecimalArrayOne(i) = CDec(strNumbers(i))
                        DialogResult = DialogResult.None
                    End If
                Next

                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subDecimal_RangeArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            ReturnedDecimalArrayOne = Nothing
            ReturnedDecimalArrayTwo = Nothing
            txtInput.Text = CStr(Decimal.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = False

            Dim strDecimalRange()() As String = New String(strInputLines.Length - 1)() {}
            ReturnedDecimalArrayOne = Nothing
            ReturnedDecimalArrayTwo = Nothing

            For j = 0 To strInputLines.Length - 1
                Dim Delimiter As String = DelimiterFinder(strInputLines(j))
                If Delimiter <> String.Empty Then
                    strDecimalRange(j) = Split(strInputLines(j), Delimiter)

                    If strDecimalRange(j).Length = 2 Then
                        Dim strDecimalRangeNew() = SimplifyArray(strDecimalRange(j), isOverallNumeric) '  Lets try to transform the text into numbers

                        If Not isOverallNumeric Then
                            ForcedExitSub = True
                            DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                            Exit For

                        Else
                            If RoundNum <> -1 Then strDecimalRangeNew = NormalRound(strDecimalRangeNew, CUInt(RoundNum), MinNumLength, , AtLeastOneBecameZeroByRounding) 'Rounding the Number if we must

                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                            If MinimumValidDecimal <> Decimal.MinValue AndAlso ((CDec(strDecimalRangeNew(0)) <= CDec(strDecimalRangeNew(1)) AndAlso CDec(strDecimalRangeNew(0)) < MinimumValidDecimal) OrElse (CDec(strDecimalRangeNew(0)) > CDec(strDecimalRangeNew(1)) AndAlso CDec(strDecimalRangeNew(1)) < MinimumValidDecimal)) Then
                                MsgBox(strLanguage_Typebox(41).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strDecimalRangeNew Order By CDbl(Num) Ascending).FirstOrDefault) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                DialogResult = DialogResult.None
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                            ElseIf MaximumValidDecimal <> Decimal.MinValue AndAlso ((CDec(strDecimalRangeNew(0)) <= CDec(strDecimalRangeNew(1)) AndAlso CDec(strDecimalRangeNew(1)) > MaximumValidDecimal) OrElse (CDec(strDecimalRangeNew(0)) > CDec(strDecimalRangeNew(1)) AndAlso CDec(strDecimalRangeNew(0)) > MaximumValidDecimal)) Then
                                MsgBox(strLanguage_Typebox(42).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strDecimalRangeNew Order By CDbl(Num) Descending).FirstOrDefault) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                            Else 'If everything is right:
                                ReDim Preserve ReturnedDecimalArrayOne(j)
                                ReturnedDecimalArrayOne(j) = CDec(strDecimalRangeNew(0))
                                ReDim Preserve ReturnedDecimalArrayTwo(j)
                                ReturnedDecimalArrayTwo(j) = CDec(strDecimalRangeNew(1))
                            End If

                        End If


                    Else
                        MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        ForcedExitSub = True
                        DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                        Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP
                    End If

                Else
                    MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    ForcedExitSub = True
                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                    Exit Sub 'To avoid the Overall Numeric Check AND the LOOP
                End If
            Next

            If isOverallNumeric Then
                'Text's Lines have been found all valid number-ranges, now the whole lines() will be returned
                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
            End If
        End If
    End Sub
#End Region

#Region "Single"
    Private Sub subSingle_Value()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            txtInput.Text = CStr(Single.MinValue)
            ReturnedSingleOne = Single.MinValue
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strNumber As String = SimplifyValue(strUserInput).ToString '   Lets try to transform the text into a number

            If RoundNum <> -1 Then strNumber = NormalRound(strUserInput, CUInt(RoundNum), MinNumLength, AtLeastOneBecameZeroByRounding) '    Rounding the Number if we must

            If Not IsNumeric(strNumber) Then '                      If it failed, lets warn the user
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation) 'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None

                '                                                   If the number is outside the bounds, lets warn the user
            ElseIf (MinimumValidSingle <> Single.MinValue AndAlso CSng(strNumber) < MinimumValidSingle) OrElse (MaximumValidSingle <> Single.MinValue AndAlso CSng(strNumber) > MaximumValidSingle) Then
                MsgBox(strLanguage_Typebox(26) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]") 'The number you typed is outside the bounds of the valid range:
                DialogResult = DialogResult.None

                '                                                   Else, everything's ok
            Else
                ReturnedSingleOne = CSng(strNumber)
                DialogResult = DialogResult.OK
                Close()
            End If
        End If
    End Sub

    Private Sub subSingle_Range()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            ReturnedSingleOne = Single.MinValue
            ReturnedSingleTwo = Single.MinValue
            txtInput.Text = CStr(Single.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim Delimiter As String = DelimiterFinder(strUserInput)
            If Delimiter <> String.Empty Then
                Dim strSingleRange() As String = Split(strUserInput, Delimiter)

                If strSingleRange.Length <> 2 Then 'If it isn't a real range, lets warn the user
                    MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    txtInput.Text = ""
                    DialogResult = DialogResult.None

                Else
                    Dim isTheRangeComprisedOfOnlyNums As Boolean 'This is separate so as not to confuse the mind with the "Array" word of the func
                    Dim strSingleRangeNew() As String = SimplifyArray(strSingleRange, isTheRangeComprisedOfOnlyNums) '  Lets try to transform the text into numbers

                    If isTheRangeComprisedOfOnlyNums Then
                        If RoundNum <> -1 Then strSingleRangeNew = NormalRound(strSingleRangeNew, CUInt(RoundNum), MinNumLength, , AtLeastOneBecameZeroByRounding) '  Rounding the Number if we must

                        'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                        If MinimumValidSingle <> Single.MinValue AndAlso ((CSng(strSingleRangeNew(0)) <= CSng(strSingleRangeNew(1)) AndAlso CSng(strSingleRangeNew(0)) < MinimumValidSingle) OrElse (CSng(strSingleRangeNew(0)) > CSng(strSingleRangeNew(1)) AndAlso CSng(strSingleRangeNew(1)) < MinimumValidSingle)) Then
                            MsgBox(strLanguage_Typebox(28) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                            DialogResult = DialogResult.None

                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                        ElseIf MaximumValidSingle <> Single.MinValue AndAlso ((CSng(strSingleRangeNew(0)) <= CSng(strSingleRangeNew(1)) AndAlso CSng(strSingleRangeNew(1)) > MaximumValidSingle) OrElse (CSng(strSingleRangeNew(0)) > CSng(strSingleRangeNew(1)) AndAlso CSng(strSingleRangeNew(0)) > MaximumValidSingle)) Then
                            MsgBox(strLanguage_Typebox(29) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                            DialogResult = DialogResult.None

                            '                                                   Else, everything's ok
                        Else
                            ReturnedSingleOne = CSng(strSingleRangeNew(0))
                            ReturnedSingleTwo = CSng(strSingleRangeNew(1))
                            DialogResult = DialogResult.OK
                            Close()

                        End If

                    Else
                        MsgBox(strLanguage_Typebox(18), MsgBoxStyle.Exclamation) 'You must only type numbers, no letters are allowed.
                        DialogResult = DialogResult.None
                    End If

                End If

            Else
                MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subSingle_ValueArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            ReturnedSingleArrayOne = Nothing
            txtInput.Text = CStr(Single.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = True

            Dim strNumbers() As String = SimplifyArray(strInputLines, isOverallNumeric) '  Lets try to transform the text into numbers

            If isOverallNumeric = True Then
                If RoundNum <> -1 Then strNumbers = NormalRound(strNumbers, CUInt(RoundNum), MinNumLength, , AtLeastOneBecameZeroByRounding) 'Rounding the Number if we must

                For i = 0 To strNumbers.Length - 1
                    '                                                       If the number is outside the bounds, lets warn the user
                    If (MinimumValidSingle <> Single.MinValue AndAlso CSng(strNumbers(i)) < MinimumValidSingle) OrElse (MaximumValidSingle <> Single.MinValue AndAlso CSng(strNumbers(i)) > MaximumValidSingle) Then
                        MsgBox(strLanguage_Typebox(40).Replace("{Line}", (i + 1).ToString).Replace("{Num}", strNumbers(i)) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]", MsgBoxStyle.Exclamation) 'The number you typed is outside the bounds of the valid range:
                        ForcedExitSub = True
                        DialogResult = DialogResult.None
                        Exit Sub 'To Avoid the Overall Numeric Check

                        '                                                   Else, everything's ok
                    Else
                        ReDim Preserve ReturnedSingleArrayOne(i)
                        ReturnedSingleArrayOne(i) = CSng(strNumbers(i))
                        DialogResult = DialogResult.None
                    End If
                Next

                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subSingle_RangeArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then 'If the text is empty, and we allow it, then everything's ok
            ReturnedSingleArrayOne = Nothing
            ReturnedSingleArrayTwo = Nothing
            txtInput.Text = CStr(Single.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = False

            Dim strSingleRange()() As String = New String(strInputLines.Length - 1)() {}
            ReturnedSingleArrayOne = Nothing
            ReturnedSingleArrayTwo = Nothing

            For j = 0 To strInputLines.Length - 1
                Dim Delimiter As String = DelimiterFinder(strInputLines(j))
                If Delimiter <> String.Empty Then
                    strSingleRange(j) = Split(strInputLines(j), Delimiter)

                    If strSingleRange(j).Length = 2 Then
                        Dim strSingleRangeNew() = SimplifyArray(strSingleRange(j), isOverallNumeric) '  Lets try to transform the text into numbers

                        If Not isOverallNumeric Then
                            ForcedExitSub = True
                            DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                            Exit For

                        Else
                            If RoundNum <> -1 Then strSingleRangeNew = NormalRound(strSingleRangeNew, CUInt(RoundNum), MinNumLength, , AtLeastOneBecameZeroByRounding) 'Rounding the Number if we must

                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                            If MinimumValidSingle <> Single.MinValue AndAlso ((CSng(strSingleRangeNew(0)) <= CSng(strSingleRangeNew(1)) AndAlso CSng(strSingleRangeNew(0)) < MinimumValidSingle) OrElse (CSng(strSingleRangeNew(0)) > CSng(strSingleRangeNew(1)) AndAlso CSng(strSingleRangeNew(1)) < MinimumValidSingle)) Then
                                MsgBox(strLanguage_Typebox(41).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strSingleRangeNew Order By CDbl(Num) Ascending).FirstOrDefault) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                DialogResult = DialogResult.None
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                            ElseIf MaximumValidSingle <> Single.MinValue AndAlso ((CSng(strSingleRangeNew(0)) <= CSng(strSingleRangeNew(1)) AndAlso CSng(strSingleRangeNew(1)) > MaximumValidSingle) OrElse (CSng(strSingleRangeNew(0)) > CSng(strSingleRangeNew(1)) AndAlso CSng(strSingleRangeNew(0)) > MaximumValidSingle)) Then
                                MsgBox(strLanguage_Typebox(42).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strSingleRangeNew Order By CDbl(Num) Descending).FirstOrDefault) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                            Else 'If everything is right:
                                ReDim Preserve ReturnedSingleArrayOne(j)
                                ReturnedSingleArrayOne(j) = CSng(strSingleRangeNew(0))
                                ReDim Preserve ReturnedSingleArrayTwo(j)
                                ReturnedSingleArrayTwo(j) = CSng(strSingleRangeNew(1))
                            End If

                        End If


                    Else
                        MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        ForcedExitSub = True
                        DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                        Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP
                    End If

                Else
                    MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    ForcedExitSub = True
                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                    Exit Sub 'To avoid the Overall Numeric Check AND the LOOP
                End If
            Next

            If isOverallNumeric Then
                'Text's Lines have been found all valid number-ranges, now the whole lines() will be returned
                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
            End If
        End If
    End Sub
#End Region

#Region "Integer"
    Private Sub subInteger_Value()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            txtInput.Text = CStr(Integer.MinValue)
            ReturnedIntegerOne = Integer.MinValue
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strNumber As String = String.Empty
            Try
                strNumber = MathEvaluator.SimplifyObject(strUserInput).ToString
            Catch ex As Exception
            End Try

            If Not IsNumeric(strNumber) Then
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation) 'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None
            Else
                If (MinimumValidInteger <> Integer.MinValue AndAlso CInt(strNumber) < MinimumValidInteger) OrElse (MaximumValidInteger <> Integer.MinValue AndAlso CInt(strNumber) > MaximumValidInteger) Then
                    MsgBox(strLanguage_Typebox(26) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]") 'The number you typed is outside the bounds of the valid range:
                    DialogResult = DialogResult.None
                Else
                    ReturnedIntegerOne = CInt(strNumber)
                    DialogResult = DialogResult.OK
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub subInteger_Range()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedIntegerOne = Integer.MinValue
            ReturnedIntegerTwo = Integer.MinValue
            txtInput.Text = CStr(Integer.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim Delimiter As String = DelimiterFinder(strUserInput)
            If Delimiter <> String.Empty Then
                Dim strIntegerRange() As String = Split(strUserInput, Delimiter)

                If strIntegerRange.Length <> 2 Then
                    MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    txtInput.Text = ""
                    DialogResult = DialogResult.None

                Else
                    Dim isTheRangeComprisedOfOnlyNums As Boolean 'This is separate so as not to confuse the mind with the "Array" word of the func
                    Dim strIntegerRangeNew() As String = SimplifyArray(strIntegerRange, isTheRangeComprisedOfOnlyNums)

                    If isTheRangeComprisedOfOnlyNums Then
                        'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                        If MinimumValidInteger <> Integer.MinValue AndAlso ((CInt(strIntegerRangeNew(0)) <= CInt(strIntegerRangeNew(1)) AndAlso CInt(strIntegerRangeNew(0)) < MinimumValidInteger) OrElse (CInt(strIntegerRangeNew(0)) > CInt(strIntegerRangeNew(1)) AndAlso CInt(strIntegerRangeNew(1)) < MinimumValidInteger)) Then
                            MsgBox(strLanguage_Typebox(28) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                            DialogResult = DialogResult.None

                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                        ElseIf MaximumValidInteger <> Integer.MinValue AndAlso ((CInt(strIntegerRangeNew(0)) <= CInt(strIntegerRangeNew(1)) AndAlso CInt(strIntegerRangeNew(1)) > MaximumValidInteger) OrElse (CInt(strIntegerRangeNew(0)) > CInt(strIntegerRangeNew(1)) AndAlso CInt(strIntegerRangeNew(0)) > MaximumValidInteger)) Then
                            MsgBox(strLanguage_Typebox(29) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                            DialogResult = DialogResult.None

                        Else
                            ReturnedIntegerOne = CInt(strIntegerRangeNew(0))
                            ReturnedIntegerTwo = CInt(strIntegerRangeNew(1))
                            DialogResult = DialogResult.OK
                            Close()

                        End If

                    Else
                        MsgBox(strLanguage_Typebox(18), MsgBoxStyle.Exclamation) 'You must only type numbers, no letters are allowed.
                        DialogResult = DialogResult.None
                    End If

                End If

            Else
                MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subInteger_ValueArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedIntegerArrayOne = Nothing
            txtInput.Text = CStr(Integer.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = True

            Dim strNumbers() As String = SimplifyArray(strInputLines, isOverallNumeric)

            If isOverallNumeric = True Then
                For i = 0 To strNumbers.Length - 1
                    If (MinimumValidInteger <> Integer.MinValue AndAlso CInt(strNumbers(i)) < MinimumValidInteger) OrElse (MaximumValidInteger <> Integer.MinValue AndAlso CInt(strNumbers(i)) > MaximumValidInteger) Then
                        MsgBox(strLanguage_Typebox(40).Replace("{Line}", (i + 1).ToString).Replace("{Num}", strNumbers(i)) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]", MsgBoxStyle.Exclamation) 'The number you typed is outside the bounds of the valid range:
                        ForcedExitSub = True
                        DialogResult = DialogResult.None
                        Exit Sub 'To Avoid the Overall Numeric Check

                    Else
                        ReDim Preserve ReturnedIntegerArrayOne(i)
                        ReturnedIntegerArrayOne(i) = CInt(strNumbers(i))
                        DialogResult = DialogResult.None
                    End If
                Next

                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subInteger_RangeArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedIntegerArrayOne = Nothing
            ReturnedIntegerArrayTwo = Nothing
            txtInput.Text = CStr(Integer.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = False

            Dim strIntegerRange()() As String = New String(strInputLines.Length - 1)() {}
            ReturnedIntegerArrayOne = Nothing
            ReturnedIntegerArrayTwo = Nothing

            For j = 0 To strInputLines.Length - 1
                Dim Delimiter As String = DelimiterFinder(strInputLines(j))
                If Delimiter <> String.Empty Then
                    strIntegerRange(j) = Split(strInputLines(j), Delimiter)

                    If strIntegerRange(j).Length = 2 Then
                        Dim strIntegerRangeNew() = SimplifyArray(strIntegerRange(j), isOverallNumeric)

                        If Not isOverallNumeric Then
                            ForcedExitSub = True
                            DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                            Exit For

                        Else
                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                            If MinimumValidInteger <> Integer.MinValue AndAlso ((CInt(strIntegerRangeNew(0)) <= CInt(strIntegerRangeNew(1)) AndAlso CInt(strIntegerRangeNew(0)) < MinimumValidInteger) OrElse (CInt(strIntegerRangeNew(0)) > CInt(strIntegerRangeNew(1)) AndAlso CInt(strIntegerRangeNew(1)) < MinimumValidInteger)) Then
                                MsgBox(strLanguage_Typebox(41).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strIntegerRangeNew Order By CDbl(Num) Ascending).FirstOrDefault) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                DialogResult = DialogResult.None
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                            ElseIf MaximumValidInteger <> Integer.MinValue AndAlso ((CInt(strIntegerRangeNew(0)) <= CInt(strIntegerRangeNew(1)) AndAlso CInt(strIntegerRangeNew(1)) > MaximumValidInteger) OrElse (CInt(strIntegerRangeNew(0)) > CInt(strIntegerRangeNew(1)) AndAlso CInt(strIntegerRangeNew(0)) > MaximumValidInteger)) Then
                                MsgBox(strLanguage_Typebox(42).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strIntegerRangeNew Order By CDbl(Num) Descending).FirstOrDefault) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                            Else 'If everything is right:
                                ReDim Preserve ReturnedIntegerArrayOne(j)
                                ReturnedIntegerArrayOne(j) = CInt(strIntegerRangeNew(0))
                                ReDim Preserve ReturnedIntegerArrayTwo(j)
                                ReturnedIntegerArrayTwo(j) = CInt(strIntegerRangeNew(1))
                            End If

                        End If


                    Else
                        MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        ForcedExitSub = True
                        DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                        Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP
                    End If

                Else
                    MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    ForcedExitSub = True
                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                    Exit Sub 'To avoid the Overall Numeric Check AND the LOOP
                End If
            Next

            If isOverallNumeric Then
                'Text's Lines have been found all valid number-ranges, now the whole lines() will be returned
                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
            End If
        End If
    End Sub
#End Region

#Region "UInteger"
    Private Sub subUInteger_Value()
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                txtInput.Text = CStr(-1)
                ReturnedUIntegerOne = UInteger.MaxValue
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim strNumber As String = String.Empty
                Try
                    strNumber = MathEvaluator.SimplifyObject(strUserInput).ToString
                Catch ex As Exception
                End Try

                If Not IsNumeric(strNumber) Then
                    MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation) 'Only numeric values allowed
                    txtInput.Text = ""
                    DialogResult = DialogResult.None
                Else
                    If (MinimumValidUInteger <> UInteger.MaxValue AndAlso CUInt(strNumber) < MinimumValidUInteger) OrElse (MaximumValidUInteger <> UInteger.MaxValue AndAlso CUInt(strNumber) > MaximumValidUInteger) Then
                        MsgBox(strLanguage_Typebox(26) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]") 'The number you typed is outside the bounds of the valid range:
                        DialogResult = DialogResult.None
                    Else
                        ReturnedUIntegerOne = CUInt(strNumber)
                        DialogResult = DialogResult.OK
                        Close()
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subUInteger_Range()
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                ReturnedUIntegerOne = UInteger.MaxValue
                ReturnedUIntegerTwo = UInteger.MaxValue
                txtInput.Text = CStr(-1)
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim Delimiter As String = DelimiterFinder(strUserInput)
                If Delimiter <> String.Empty Then
                    Dim strUIntegerRange() As String = Split(strUserInput, Delimiter)

                    If strUIntegerRange.Length <> 2 Then
                        MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        txtInput.Text = ""
                        DialogResult = DialogResult.None

                    Else
                        Dim isTheRangeComprisedOfOnlyNums As Boolean 'This is separate so as not to confuse the mind with the "Array" word of the func
                        Dim strUIntegerRangeNew() As String = SimplifyArray(strUIntegerRange, isTheRangeComprisedOfOnlyNums)

                        If isTheRangeComprisedOfOnlyNums Then
                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                            If MinimumValidUInteger <> UInteger.MaxValue AndAlso ((CUInt(strUIntegerRangeNew(0)) <= CUInt(strUIntegerRangeNew(1)) AndAlso CUInt(strUIntegerRangeNew(0)) < MinimumValidUInteger) OrElse (CUInt(strUIntegerRangeNew(0)) > CUInt(strUIntegerRangeNew(1)) AndAlso CUInt(strUIntegerRangeNew(1)) < MinimumValidUInteger)) Then
                                MsgBox(strLanguage_Typebox(28) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                DialogResult = DialogResult.None

                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                            ElseIf MaximumValidUInteger <> UInteger.MaxValue AndAlso ((CUInt(strUIntegerRangeNew(0)) <= CUInt(strUIntegerRangeNew(1)) AndAlso CUInt(strUIntegerRangeNew(1)) > MaximumValidUInteger) OrElse (CUInt(strUIntegerRangeNew(0)) > CUInt(strUIntegerRangeNew(1)) AndAlso CUInt(strUIntegerRangeNew(0)) > MaximumValidUInteger)) Then
                                MsgBox(strLanguage_Typebox(29) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                DialogResult = DialogResult.None

                            Else
                                ReturnedUIntegerOne = CUInt(strUIntegerRangeNew(0))
                                ReturnedUIntegerTwo = CUInt(strUIntegerRangeNew(1))
                                DialogResult = DialogResult.OK
                                Close()

                            End If

                        Else
                            MsgBox(strLanguage_Typebox(18), MsgBoxStyle.Exclamation) 'You must only type numbers, no letters are allowed.
                            DialogResult = DialogResult.None
                        End If

                    End If

                Else
                    MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    txtInput.Text = ""
                    DialogResult = DialogResult.None
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subUInteger_ValueArray(ByRef ForcedExitSub As Boolean)
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                ReturnedUIntegerArrayOne = Nothing
                txtInput.Text = CStr(UInteger.MaxValue)
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim strInputLines() As String = strUserInputs
                Dim isOverallNumeric As Boolean = True

                Dim strNumbers() As String = SimplifyArray(strInputLines, isOverallNumeric)

                If isOverallNumeric = True Then
                    For i = 0 To strNumbers.Length - 1
                        If (MinimumValidUInteger <> UInteger.MaxValue AndAlso CUInt(strNumbers(i)) < MinimumValidUInteger) OrElse (MaximumValidUInteger <> UInteger.MaxValue AndAlso CUInt(strNumbers(i)) > MaximumValidUInteger) Then
                            MsgBox(strLanguage_Typebox(40).Replace("{Line}", (i + 1).ToString).Replace("{Num}", strNumbers(i)) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]", MsgBoxStyle.Exclamation) 'The number you typed is outside the bounds of the valid range:
                            ForcedExitSub = True
                            DialogResult = DialogResult.None
                            Exit Sub 'To Avoid the Overall Numeric Check

                        Else
                            ReDim Preserve ReturnedUIntegerArrayOne(i)
                            ReturnedUIntegerArrayOne(i) = CUInt(strNumbers(i))
                            DialogResult = DialogResult.None
                        End If
                    Next

                    DialogResult = DialogResult.OK
                    Close()

                Else
                    MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                    txtInput.Text = ""
                    DialogResult = DialogResult.None
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subUInteger_RangeArray(ByRef ForcedExitSub As Boolean)
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                ReturnedUIntegerArrayOne = Nothing
                ReturnedUIntegerArrayTwo = Nothing
                txtInput.Text = CStr(UInteger.MaxValue)
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim strInputLines() As String = strUserInputs
                Dim isOverallNumeric As Boolean = False

                Dim strUIntegerRange()() As String = New String(strInputLines.Length - 1)() {}
                ReturnedUIntegerArrayOne = Nothing
                ReturnedUIntegerArrayTwo = Nothing

                For j = 0 To strInputLines.Length - 1
                    Dim Delimiter As String = DelimiterFinder(strInputLines(j))
                    If Delimiter <> String.Empty Then
                        strUIntegerRange(j) = Split(strInputLines(j), Delimiter)

                        If strUIntegerRange(j).Length = 2 Then
                            Dim strUIntegerRangeNew() = SimplifyArray(strUIntegerRange(j), isOverallNumeric)

                            If Not isOverallNumeric Then
                                ForcedExitSub = True
                                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                Exit For

                            Else
                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                                If MinimumValidUInteger <> UInteger.MinValue AndAlso ((CUInt(strUIntegerRangeNew(0)) <= CUInt(strUIntegerRangeNew(1)) AndAlso CUInt(strUIntegerRangeNew(0)) < MinimumValidUInteger) OrElse (CUInt(strUIntegerRangeNew(0)) > CUInt(strUIntegerRangeNew(1)) AndAlso CUInt(strUIntegerRangeNew(1)) < MinimumValidUInteger)) Then
                                    MsgBox(strLanguage_Typebox(41).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strUIntegerRangeNew Order By CDbl(Num) Ascending).FirstOrDefault) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                    DialogResult = DialogResult.None
                                    ForcedExitSub = True
                                    Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                    'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                                ElseIf MaximumValidUInteger <> UInteger.MinValue AndAlso ((CUInt(strUIntegerRangeNew(0)) <= CUInt(strUIntegerRangeNew(1)) AndAlso CUInt(strUIntegerRangeNew(1)) > MaximumValidUInteger) OrElse (CUInt(strUIntegerRangeNew(0)) > CUInt(strUIntegerRangeNew(1)) AndAlso CUInt(strUIntegerRangeNew(0)) > MaximumValidUInteger)) Then
                                    MsgBox(strLanguage_Typebox(42).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strUIntegerRangeNew Order By CDbl(Num) Descending).FirstOrDefault) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                    ForcedExitSub = True
                                    Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                Else 'If everything is right:
                                    ReDim Preserve ReturnedUIntegerArrayOne(j)
                                    ReturnedUIntegerArrayOne(j) = CUInt(strUIntegerRangeNew(0))
                                    ReDim Preserve ReturnedUIntegerArrayTwo(j)
                                    ReturnedUIntegerArrayTwo(j) = CUInt(strUIntegerRangeNew(1))
                                End If

                            End If


                        Else
                            MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                            ForcedExitSub = True
                            DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                            Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP
                        End If

                    Else
                        MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        ForcedExitSub = True
                        DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                        Exit Sub 'To avoid the Overall Numeric Check AND the LOOP
                    End If
                Next

                If isOverallNumeric Then
                    'Text's Lines have been found all valid number-ranges, now the whole lines() will be returned
                    DialogResult = DialogResult.OK
                    Close()

                Else
                    MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Short"
    Private Sub subShort_Value()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            txtInput.Text = CStr(Short.MinValue)
            ReturnedShortOne = Short.MinValue
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strNumber As String = String.Empty
            Try
                strNumber = MathEvaluator.SimplifyObject(strUserInput).ToString
            Catch ex As Exception
            End Try

            If Not IsNumeric(strNumber) Then
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation) 'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None
            Else
                If (MinimumValidShort <> Short.MinValue AndAlso CShort(strNumber) < MinimumValidShort) OrElse (MaximumValidShort <> Short.MinValue AndAlso CShort(strNumber) > MaximumValidShort) Then
                    MsgBox(strLanguage_Typebox(26) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]") 'The number you typed is outside the bounds of the valid range:
                    DialogResult = DialogResult.None
                Else
                    ReturnedShortOne = CShort(strNumber)
                    DialogResult = DialogResult.OK
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub subShort_Range()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedShortOne = Short.MinValue
            ReturnedShortTwo = Short.MinValue
            txtInput.Text = CStr(Short.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim Delimiter As String = DelimiterFinder(strUserInput)
            If Delimiter <> String.Empty Then
                Dim strShortRange() As String = Split(strUserInput, Delimiter)

                If strShortRange.Length <> 2 Then
                    MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    txtInput.Text = ""
                    DialogResult = DialogResult.None

                Else
                    Dim isTheRangeComprisedOfOnlyNums As Boolean 'This is separate so as not to confuse the mind with the "Array" word of the func
                    Dim strShortRangeNew() As String = SimplifyArray(strShortRange, isTheRangeComprisedOfOnlyNums)

                    If isTheRangeComprisedOfOnlyNums Then
                        'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                        If MinimumValidShort <> Short.MinValue AndAlso ((CShort(strShortRangeNew(0)) <= CShort(strShortRangeNew(1)) AndAlso CShort(strShortRangeNew(0)) < MinimumValidShort) OrElse (CShort(strShortRangeNew(0)) > CShort(strShortRangeNew(1)) AndAlso CShort(strShortRangeNew(1)) < MinimumValidShort)) Then
                            MsgBox(strLanguage_Typebox(28) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                            DialogResult = DialogResult.None

                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                        ElseIf MaximumValidShort <> Short.MinValue AndAlso ((CShort(strShortRangeNew(0)) <= CShort(strShortRangeNew(1)) AndAlso CShort(strShortRangeNew(1)) > MaximumValidShort) OrElse (CShort(strShortRangeNew(0)) > CShort(strShortRangeNew(1)) AndAlso CShort(strShortRangeNew(0)) > MaximumValidShort)) Then
                            MsgBox(strLanguage_Typebox(29) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                            DialogResult = DialogResult.None

                        Else
                            ReturnedShortOne = CShort(strShortRangeNew(0))
                            ReturnedShortTwo = CShort(strShortRangeNew(1))
                            DialogResult = DialogResult.OK
                            Close()

                        End If

                    Else
                        MsgBox(strLanguage_Typebox(18), MsgBoxStyle.Exclamation) 'You must only type numbers, no letters are allowed.
                        DialogResult = DialogResult.None
                    End If

                End If

            Else
                MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subShort_ValueArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedShortArrayOne = Nothing
            txtInput.Text = CStr(Short.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = True

            Dim strNumbers() As String = SimplifyArray(strInputLines, isOverallNumeric)

            If isOverallNumeric = True Then
                For i = 0 To strNumbers.Length - 1
                    If (MinimumValidShort <> Short.MinValue AndAlso CShort(strNumbers(i)) < MinimumValidShort) OrElse (MaximumValidShort <> Short.MinValue AndAlso CShort(strNumbers(i)) > MaximumValidShort) Then
                        MsgBox(strLanguage_Typebox(40).Replace("{Line}", (i + 1).ToString).Replace("{Num}", strNumbers(i)) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]", MsgBoxStyle.Exclamation) 'The number you typed is outside the bounds of the valid range:
                        ForcedExitSub = True
                        DialogResult = DialogResult.None
                        Exit Sub 'To Avoid the Overall Numeric Check

                    Else
                        ReDim Preserve ReturnedShortArrayOne(i)
                        ReturnedShortArrayOne(i) = CShort(strNumbers(i))
                        DialogResult = DialogResult.None
                    End If
                Next

                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subShort_RangeArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedShortArrayOne = Nothing
            ReturnedShortArrayTwo = Nothing
            txtInput.Text = CStr(Short.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = False

            Dim strShortRange()() As String = New String(strInputLines.Length - 1)() {}
            ReturnedShortArrayOne = Nothing
            ReturnedShortArrayTwo = Nothing

            For j = 0 To strInputLines.Length - 1
                Dim Delimiter As String = DelimiterFinder(strInputLines(j))
                If Delimiter <> String.Empty Then
                    strShortRange(j) = Split(strInputLines(j), Delimiter)

                    If strShortRange(j).Length = 2 Then
                        Dim strShortRangeNew() = SimplifyArray(strShortRange(j), isOverallNumeric)

                        If Not isOverallNumeric Then
                            ForcedExitSub = True
                            DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                            Exit For

                        Else
                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                            If MinimumValidShort <> Short.MinValue AndAlso ((CShort(strShortRangeNew(0)) <= CShort(strShortRangeNew(1)) AndAlso CShort(strShortRangeNew(0)) < MinimumValidShort) OrElse (CShort(strShortRangeNew(0)) > CShort(strShortRangeNew(1)) AndAlso CShort(strShortRangeNew(1)) < MinimumValidShort)) Then
                                MsgBox(strLanguage_Typebox(41).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strShortRangeNew Order By CDbl(Num) Ascending).FirstOrDefault) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                DialogResult = DialogResult.None
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                            ElseIf MaximumValidShort <> Short.MinValue AndAlso ((CShort(strShortRangeNew(0)) <= CShort(strShortRangeNew(1)) AndAlso CShort(strShortRangeNew(1)) > MaximumValidShort) OrElse (CShort(strShortRangeNew(0)) > CShort(strShortRangeNew(1)) AndAlso CShort(strShortRangeNew(0)) > MaximumValidShort)) Then
                                MsgBox(strLanguage_Typebox(42).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strShortRangeNew Order By CDbl(Num) Descending).FirstOrDefault) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                            Else 'If everything is right:
                                ReDim Preserve ReturnedShortArrayOne(j)
                                ReturnedShortArrayOne(j) = CShort(strShortRangeNew(0))
                                ReDim Preserve ReturnedShortArrayTwo(j)
                                ReturnedShortArrayTwo(j) = CShort(strShortRangeNew(1))
                            End If

                        End If


                    Else
                        MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        ForcedExitSub = True
                        DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                        Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP
                    End If

                Else
                    MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    ForcedExitSub = True
                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                    Exit Sub 'To avoid the Overall Numeric Check AND the LOOP
                End If
            Next

            If isOverallNumeric Then
                'Text's Lines have been found all valid number-ranges, now the whole lines() will be returned
                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
            End If
        End If
    End Sub
#End Region

#Region "UShort"
    Private Sub subUShort_Value()
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                txtInput.Text = CStr(-1)
                ReturnedUShortOne = UShort.MaxValue
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim strNumber As String = String.Empty
                Try
                    strNumber = MathEvaluator.SimplifyObject(strUserInput).ToString
                Catch ex As Exception
                End Try

                If Not IsNumeric(strNumber) Then
                    MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation) 'Only numeric values allowed
                    txtInput.Text = ""
                    DialogResult = DialogResult.None
                Else
                    If (MinimumValidUShort <> UShort.MaxValue AndAlso CUShort(strNumber) < MinimumValidUShort) OrElse (MaximumValidUShort <> UShort.MaxValue AndAlso CUShort(strNumber) > MaximumValidUShort) Then
                        MsgBox(strLanguage_Typebox(26) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]") 'The number you typed is outside the bounds of the valid range:
                        DialogResult = DialogResult.None
                    Else
                        ReturnedUShortOne = CUShort(strNumber)
                        DialogResult = DialogResult.OK
                        Close()
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subUShort_Range()
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                ReturnedUShortOne = UShort.MaxValue
                ReturnedUShortTwo = UShort.MaxValue
                txtInput.Text = CStr(-1)
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim Delimiter As String = DelimiterFinder(strUserInput)
                If Delimiter <> String.Empty Then
                    Dim strUShortRange() As String = Split(strUserInput, Delimiter)

                    If strUShortRange.Length <> 2 Then
                        MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        txtInput.Text = ""
                        DialogResult = DialogResult.None

                    Else
                        Dim isTheRangeComprisedOfOnlyNums As Boolean 'This is separate so as not to confuse the mind with the "Array" word of the func
                        Dim strUShortRangeNew() As String = SimplifyArray(strUShortRange, isTheRangeComprisedOfOnlyNums)

                        If isTheRangeComprisedOfOnlyNums Then
                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                            If MinimumValidUShort <> UShort.MaxValue AndAlso ((CUShort(strUShortRangeNew(0)) <= CUShort(strUShortRangeNew(1)) AndAlso CUShort(strUShortRangeNew(0)) < MinimumValidUShort) OrElse (CUShort(strUShortRangeNew(0)) > CUShort(strUShortRangeNew(1)) AndAlso CUShort(strUShortRangeNew(1)) < MinimumValidUShort)) Then
                                MsgBox(strLanguage_Typebox(28) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                DialogResult = DialogResult.None

                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                            ElseIf MaximumValidUShort <> UShort.MaxValue AndAlso ((CUShort(strUShortRangeNew(0)) <= CUShort(strUShortRangeNew(1)) AndAlso CUShort(strUShortRangeNew(1)) > MaximumValidUShort) OrElse (CUShort(strUShortRangeNew(0)) > CUShort(strUShortRangeNew(1)) AndAlso CUShort(strUShortRangeNew(0)) > MaximumValidUShort)) Then
                                MsgBox(strLanguage_Typebox(29) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                DialogResult = DialogResult.None

                            Else
                                ReturnedUShortOne = CUShort(strUShortRangeNew(0).Replace("""", ""))
                                ReturnedUShortTwo = CUShort(strUShortRangeNew(1).Replace("""", ""))
                                DialogResult = DialogResult.OK
                                Close()

                            End If

                        Else
                            MsgBox(strLanguage_Typebox(18), MsgBoxStyle.Exclamation) 'You must only type numbers, no letters are allowed.
                            DialogResult = DialogResult.None
                        End If

                    End If

                Else
                    MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    txtInput.Text = ""
                    DialogResult = DialogResult.None
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub subUShort_ValueArray(ByRef ForcedExitSub As Boolean)
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                ReturnedUShortArrayOne = Nothing
                txtInput.Text = CStr(UShort.MaxValue)
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim strInputLines() As String = strUserInputs
                Dim isOverallNumeric As Boolean = True

                Dim strNumbers() As String = SimplifyArray(strInputLines, isOverallNumeric)

                If isOverallNumeric = True Then
                    For i = 0 To strNumbers.Length - 1
                        If (MinimumValidUShort <> UShort.MaxValue AndAlso CUShort(strNumbers(i)) < MinimumValidUShort) OrElse (MaximumValidUShort <> UShort.MaxValue AndAlso CUShort(strNumbers(i)) > MaximumValidUShort) Then
                            MsgBox(strLanguage_Typebox(40).Replace("{Line}", (i + 1).ToString).Replace("{Num}", strNumbers(i)) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]", MsgBoxStyle.Exclamation) 'The number you typed is outside the bounds of the valid range:
                            ForcedExitSub = True
                            DialogResult = DialogResult.None
                            Exit Sub 'To Avoid the Overall Numeric Check

                        Else
                            ReDim Preserve ReturnedUShortArrayOne(i)
                            ReturnedUShortArrayOne(i) = CUShort(strNumbers(i))
                            DialogResult = DialogResult.None
                        End If
                    Next

                    DialogResult = DialogResult.OK
                    Close()

                Else
                    MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                    txtInput.Text = ""
                    DialogResult = DialogResult.None
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub UShort_RangeArray(ByRef ForcedExitSub As Boolean)
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                ReturnedUShortArrayOne = Nothing
                ReturnedUShortArrayTwo = Nothing
                txtInput.Text = CStr(UShort.MaxValue)
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim strInputLines() As String = strUserInputs
                Dim isOverallNumeric As Boolean = False

                Dim strUshortRange()() As String = New String(strInputLines.Length - 1)() {}
                ReturnedUShortArrayOne = Nothing
                ReturnedUShortArrayTwo = Nothing

                For j = 0 To strInputLines.Length - 1
                    Dim Delimiter As String = DelimiterFinder(strInputLines(j))
                    If Delimiter <> String.Empty Then
                        strUshortRange(j) = Split(strInputLines(j), Delimiter)

                        If strUshortRange(j).Length = 2 Then
                            Dim strUshortRangeNew() = SimplifyArray(strUshortRange(j), isOverallNumeric)

                            If Not isOverallNumeric Then
                                ForcedExitSub = True
                                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                Exit For

                            Else
                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                                If MinimumValidUShort <> UShort.MinValue AndAlso ((CUShort(strUshortRangeNew(0)) <= CUShort(strUshortRangeNew(1)) AndAlso CUShort(strUshortRangeNew(0)) < MinimumValidUShort) OrElse (CUShort(strUshortRangeNew(0)) > CUShort(strUshortRangeNew(1)) AndAlso CUShort(strUshortRangeNew(1)) < MinimumValidUShort)) Then
                                    MsgBox(strLanguage_Typebox(41).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strUshortRangeNew Order By CDbl(Num) Ascending).FirstOrDefault) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                    DialogResult = DialogResult.None
                                    ForcedExitSub = True
                                    Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                    'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                                ElseIf MaximumValidUShort <> UShort.MinValue AndAlso ((CUShort(strUshortRangeNew(0)) <= CUShort(strUshortRangeNew(1)) AndAlso CUShort(strUshortRangeNew(1)) > MaximumValidUShort) OrElse (CUShort(strUshortRangeNew(0)) > CUShort(strUshortRangeNew(1)) AndAlso CUShort(strUshortRangeNew(0)) > MaximumValidUShort)) Then
                                    MsgBox(strLanguage_Typebox(42).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strUshortRangeNew Order By CDbl(Num) Descending).FirstOrDefault) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                    ForcedExitSub = True
                                    Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                Else 'If everything is right:
                                    ReDim Preserve ReturnedUShortArrayOne(j)
                                    ReturnedUShortArrayOne(j) = CUShort(strUshortRangeNew(0))
                                    ReDim Preserve ReturnedUShortArrayTwo(j)
                                    ReturnedUShortArrayTwo(j) = CUShort(strUshortRangeNew(1))
                                End If

                            End If


                        Else
                            MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                            ForcedExitSub = True
                            DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                            Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP
                        End If

                    Else
                        MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        ForcedExitSub = True
                        DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                        Exit Sub 'To avoid the Overall Numeric Check AND the LOOP
                    End If
                Next

                If isOverallNumeric Then
                    'Text's Lines have been found all valid number-ranges, now the whole lines() will be returned
                    DialogResult = DialogResult.OK
                    Close()

                Else
                    MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Long"
    Private Sub subLong_Value()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            txtInput.Text = CStr(Long.MinValue)
            ReturnedLongOne = Long.MinValue
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strNumber As String = String.Empty
            Try
                strNumber = MathEvaluator.SimplifyObject(strUserInput).ToString
            Catch ex As Exception
            End Try

            If Not IsNumeric(strNumber) Then
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation) 'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None
            Else
                If (MinimumValidLong <> Long.MinValue AndAlso CLng(strNumber) < MinimumValidLong) OrElse (MaximumValidLong <> Long.MinValue AndAlso CLng(strNumber) > MaximumValidLong) Then
                    MsgBox(strLanguage_Typebox(26) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]") 'The number you typed is outside the bounds of the valid range:
                    DialogResult = DialogResult.None
                Else
                    ReturnedLongOne = CLng(strNumber)
                    DialogResult = DialogResult.OK
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub subLong_Range()
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedLongOne = Long.MinValue
            ReturnedLongTwo = Long.MinValue
            txtInput.Text = CStr(Long.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim Delimiter As String = DelimiterFinder(strUserInput)
            If Delimiter <> String.Empty Then
                Dim strLongRange() As String = Split(strUserInput, Delimiter)

                If strLongRange.Length <> 2 Then
                    MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    txtInput.Text = ""
                    DialogResult = DialogResult.None

                Else
                    Dim isTheRangeComprisedOfOnlyNums As Boolean 'This is separate so as not to confuse the mind with the "Array" word of the func
                    Dim strLongRangeNew() As String = SimplifyArray(strLongRange, isTheRangeComprisedOfOnlyNums)

                    If isTheRangeComprisedOfOnlyNums Then
                        'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                        If MinimumValidLong <> Long.MinValue AndAlso ((CLng(strLongRangeNew(0)) <= CLng(strLongRangeNew(1)) AndAlso CLng(strLongRangeNew(0)) < MinimumValidLong) OrElse (CLng(strLongRangeNew(0)) > CLng(strLongRangeNew(1)) AndAlso CLng(strLongRangeNew(1)) < MinimumValidLong)) Then
                            MsgBox(strLanguage_Typebox(28) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                            DialogResult = DialogResult.None

                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                        ElseIf MaximumValidLong <> Long.MinValue AndAlso ((CLng(strLongRangeNew(0)) <= CLng(strLongRangeNew(1)) AndAlso CLng(strLongRangeNew(1)) > MaximumValidLong) OrElse (CLng(strLongRangeNew(0)) > CLng(strLongRangeNew(1)) AndAlso CLng(strLongRangeNew(0)) > MaximumValidLong)) Then
                            MsgBox(strLanguage_Typebox(29) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                            DialogResult = DialogResult.None

                        Else
                            ReturnedLongOne = CLng(strLongRangeNew(0))
                            ReturnedLongTwo = CLng(strLongRangeNew(1))
                            DialogResult = DialogResult.OK
                            Close()

                        End If

                    Else
                        MsgBox(strLanguage_Typebox(18), MsgBoxStyle.Exclamation) 'You must only type numbers, no letters are allowed.
                        DialogResult = DialogResult.None
                    End If

                End If

            Else
                MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub subLong_ValueArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedLongArrayOne = Nothing
            txtInput.Text = CStr(Long.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = True

            Dim strNumbers() As String = SimplifyArray(strInputLines, isOverallNumeric)

            If isOverallNumeric = True Then
                For i = 0 To strNumbers.Length - 1
                    If (MinimumValidLong <> Long.MinValue AndAlso CLng(strNumbers(i)) < MinimumValidLong) OrElse (MaximumValidLong <> Long.MinValue AndAlso CLng(strNumbers(i)) > MaximumValidLong) Then
                        MsgBox(strLanguage_Typebox(40).Replace("{Line}", (i + 1).ToString).Replace("{Num}", strNumbers(i)) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]", MsgBoxStyle.Exclamation) 'The number you typed is outside the bounds of the valid range:
                        ForcedExitSub = True
                        DialogResult = DialogResult.None
                        Exit Sub 'To Avoid the Overall Numeric Check

                    Else
                        ReDim Preserve ReturnedLongArrayOne(i)
                        ReturnedLongArrayOne(i) = CLng(strNumbers(i))
                        DialogResult = DialogResult.None
                    End If
                Next

                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                txtInput.Text = ""
                DialogResult = DialogResult.None
            End If
        End If
    End Sub

    Private Sub Long_RangeArray(ByRef ForcedExitSub As Boolean)
        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
            ReturnedLongArrayOne = Nothing
            ReturnedLongArrayTwo = Nothing
            txtInput.Text = CStr(Long.MinValue)
            DialogResult = DialogResult.OK
            Close()

        Else
            Dim strInputLines() As String = strUserInputs
            Dim isOverallNumeric As Boolean = False

            Dim strLongRange()() As String = New String(strInputLines.Length - 1)() {}
            ReturnedLongArrayOne = Nothing
            ReturnedLongArrayTwo = Nothing

            For j = 0 To strInputLines.Length - 1
                Dim Delimiter As String = DelimiterFinder(strInputLines(j))
                If Delimiter <> String.Empty Then
                    strLongRange(j) = Split(strInputLines(j), Delimiter)

                    If strLongRange(j).Length = 2 Then
                        Dim strLongRangeNew() = SimplifyArray(strLongRange(j), isOverallNumeric)

                        If Not isOverallNumeric Then
                            ForcedExitSub = True
                            DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                            Exit For

                        Else
                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                            If MinimumValidLong <> Long.MinValue AndAlso ((CLng(strLongRangeNew(0)) <= CLng(strLongRangeNew(1)) AndAlso CLng(strLongRangeNew(0)) < MinimumValidLong) OrElse (CLng(strLongRangeNew(0)) > CLng(strLongRangeNew(1)) AndAlso CLng(strLongRangeNew(1)) < MinimumValidLong)) Then
                                MsgBox(strLanguage_Typebox(41).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strLongRangeNew Order By CDbl(Num) Ascending).FirstOrDefault) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                DialogResult = DialogResult.None
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                            ElseIf MaximumValidLong <> Long.MinValue AndAlso ((CLng(strLongRangeNew(0)) <= CLng(strLongRangeNew(1)) AndAlso CLng(strLongRangeNew(1)) > MaximumValidLong) OrElse (CLng(strLongRangeNew(0)) > CLng(strLongRangeNew(1)) AndAlso CLng(strLongRangeNew(0)) > MaximumValidLong)) Then
                                MsgBox(strLanguage_Typebox(42).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strLongRangeNew Order By CDbl(Num) Descending).FirstOrDefault) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                ForcedExitSub = True
                                Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                            Else 'If everything is right:
                                ReDim Preserve ReturnedLongArrayOne(j)
                                ReturnedLongArrayOne(j) = CLng(strLongRangeNew(0))
                                ReDim Preserve ReturnedLongArrayTwo(j)
                                ReturnedLongArrayTwo(j) = CLng(strLongRangeNew(1))
                            End If

                        End If


                    Else
                        MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        ForcedExitSub = True
                        DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                        Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP
                    End If

                Else
                    MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    ForcedExitSub = True
                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                    Exit Sub 'To avoid the Overall Numeric Check AND the LOOP
                End If
            Next

            If isOverallNumeric Then
                'Text's Lines have been found all valid number-ranges, now the whole lines() will be returned
                DialogResult = DialogResult.OK
                Close()

            Else
                MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
            End If
        End If
    End Sub
#End Region

#Region "ULong"
    Private Sub subULong_Value()
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                txtInput.Text = CStr(-1)
                ReturnedULongOne = ULong.MaxValue
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim strNumber As String = String.Empty
                Try
                    strNumber = MathEvaluator.SimplifyObject(strUserInput).ToString
                Catch ex As Exception
                End Try

                If Not IsNumeric(strNumber) Then
                    MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation) 'Only numeric values allowed
                    txtInput.Text = ""
                    DialogResult = DialogResult.None
                Else
                    If (MinimumValidULong <> ULong.MaxValue AndAlso CULng(strNumber) < MinimumValidULong) OrElse (MaximumValidULong <> ULong.MaxValue AndAlso CULng(strNumber) > MaximumValidULong) Then
                        MsgBox(strLanguage_Typebox(26) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]") 'The number you typed is outside the bounds of the valid range:
                        DialogResult = DialogResult.None
                    Else
                        ReturnedULongOne = CULng(strNumber)
                        DialogResult = DialogResult.OK
                        Close()
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subULong_Range()
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                ReturnedULongOne = ULong.MaxValue
                ReturnedULongTwo = ULong.MaxValue
                txtInput.Text = CStr(-1)
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim Delimiter As String = DelimiterFinder(strUserInput)
                If Delimiter <> String.Empty Then
                    Dim strULongRange() As String = Split(strUserInput, Delimiter)

                    If strULongRange.Length <> 2 Then
                        MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        txtInput.Text = ""
                        DialogResult = DialogResult.None

                    Else
                        Dim isTheRangeComprisedOfOnlyNums As Boolean 'This is separate so as not to confuse the mind with the "Array" word of the func
                        Dim strULongRangeNew() As String = SimplifyArray(strULongRange, isTheRangeComprisedOfOnlyNums)

                        If isTheRangeComprisedOfOnlyNums Then
                            'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                            If MinimumValidULong <> ULong.MaxValue AndAlso ((CULng(strULongRangeNew(0)) <= CULng(strULongRangeNew(1)) AndAlso CULng(strULongRangeNew(0)) < MinimumValidULong) OrElse (CULng(strULongRangeNew(0)) > CULng(strULongRangeNew(1)) AndAlso CULng(strULongRangeNew(1)) < MinimumValidULong)) Then
                                MsgBox(strLanguage_Typebox(28) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                DialogResult = DialogResult.None

                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                            ElseIf MaximumValidULong <> ULong.MaxValue AndAlso ((CULng(strULongRangeNew(0)) <= CULng(strULongRangeNew(1)) AndAlso CULng(strULongRangeNew(1)) > MaximumValidULong) OrElse (CULng(strULongRangeNew(0)) > CULng(strULongRangeNew(1)) AndAlso CULng(strULongRangeNew(0)) > MaximumValidULong)) Then
                                MsgBox(strLanguage_Typebox(29) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                DialogResult = DialogResult.None

                            Else
                                ReturnedULongOne = CULng(strULongRangeNew(0))
                                ReturnedULongTwo = CULng(strULongRangeNew(1))
                                DialogResult = DialogResult.OK
                                Close()

                            End If

                        Else
                            MsgBox(strLanguage_Typebox(18), MsgBoxStyle.Exclamation) 'You must only type numbers, no letters are allowed.
                            DialogResult = DialogResult.None
                        End If

                    End If

                Else
                    MsgBox(strLanguage_Typebox(16) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                    txtInput.Text = ""
                    DialogResult = DialogResult.None
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subULong_ValueArray(ByRef ForcedExitSub As Boolean)
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                ReturnedULongArrayOne = Nothing
                txtInput.Text = CStr(ULong.MaxValue)
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim strInputLines() As String = strUserInputs
                Dim isOverallNumeric As Boolean = True

                Dim strNumbers() As String = SimplifyArray(strInputLines, isOverallNumeric)

                If isOverallNumeric = True Then
                    For i = 0 To strNumbers.Length - 1
                        If (MinimumValidULong <> ULong.MaxValue AndAlso CULng(strNumbers(i)) < MinimumValidULong) OrElse (MaximumValidULong <> ULong.MaxValue AndAlso CULng(strNumbers(i)) > MaximumValidULong) Then
                            MsgBox(strLanguage_Typebox(40).Replace("{Line}", (i + 1).ToString).Replace("{Num}", strNumbers(i)) & vbCrLf & "[ " & strLowerLimit & " , " & strUpperLimit & " ]", MsgBoxStyle.Exclamation) 'The number you typed is outside the bounds of the valid range:
                            ForcedExitSub = True
                            DialogResult = DialogResult.None
                            Exit Sub 'To Avoid the Overall Numeric Check

                        Else
                            ReDim Preserve ReturnedULongArrayOne(i)
                            ReturnedULongArrayOne(i) = CULng(strNumbers(i))
                            DialogResult = DialogResult.None
                        End If
                    Next

                    DialogResult = DialogResult.OK
                    Close()

                Else
                    MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                    txtInput.Text = ""
                    DialogResult = DialogResult.None
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subULong_RangeArray(ByRef ForcedExitSub As Boolean)
        Try
            If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                ReturnedULongArrayOne = Nothing
                ReturnedULongArrayTwo = Nothing
                txtInput.Text = CStr(ULong.MaxValue)
                DialogResult = DialogResult.OK
                Close()

            Else
                Dim strInputLines() As String = strUserInputs
                Dim isOverallNumeric As Boolean = False

                Dim strULongRange()() As String = New String(strInputLines.Length - 1)() {}
                ReturnedULongArrayOne = Nothing
                ReturnedULongArrayTwo = Nothing

                For j = 0 To strInputLines.Length - 1
                    Dim Delimiter As String = DelimiterFinder(strInputLines(j))
                    If Delimiter <> String.Empty Then
                        strULongRange(j) = Split(strInputLines(j), Delimiter)

                        If strULongRange(j).Length = 2 Then
                            Dim strULongRangeNew() = SimplifyArray(strULongRange(j), isOverallNumeric)

                            If Not isOverallNumeric Then
                                ForcedExitSub = True
                                DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                Exit For

                            Else
                                'If the number is outside the bounds, lets warn the user                    3 to 5                                                          3 < Limit               or if                            5 to 3                                                           3 < Limit
                                If MinimumValidULong <> ULong.MinValue AndAlso ((CULng(strULongRangeNew(0)) <= CULng(strULongRangeNew(1)) AndAlso CULng(strULongRangeNew(0)) < MinimumValidULong) OrElse (CULng(strULongRangeNew(0)) > CULng(strULongRangeNew(1)) AndAlso CULng(strULongRangeNew(1)) < MinimumValidULong)) Then
                                    MsgBox(strLanguage_Typebox(41).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strULongRangeNew Order By CDbl(Num) Ascending).FirstOrDefault) & " [" & strLowerLimit & "]", MsgBoxStyle.Exclamation) 'The beginning numeric value is below the valid lower limit
                                    DialogResult = DialogResult.None
                                    ForcedExitSub = True
                                    Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                    'If the number is outside the bounds, lets warn the user                    3 to 5                                                           5 > Limit               or if                            5 to 3                                                          5 > Limit
                                ElseIf MaximumValidULong <> ULong.MinValue AndAlso ((CULng(strULongRangeNew(0)) <= CULng(strULongRangeNew(1)) AndAlso CULng(strULongRangeNew(1)) > MaximumValidULong) OrElse (CULng(strULongRangeNew(0)) > CULng(strULongRangeNew(1)) AndAlso CULng(strULongRangeNew(0)) > MaximumValidULong)) Then
                                    MsgBox(strLanguage_Typebox(42).Replace("{Line}", (j + 1).ToString).Replace("{Num}", (From Num In strULongRangeNew Order By CDbl(Num) Descending).FirstOrDefault) & " [" & strUpperLimit & "]", MsgBoxStyle.Exclamation) 'The ending numeric value is above the valid upper limit
                                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                                    ForcedExitSub = True
                                    Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP

                                Else 'If everything is right:
                                    ReDim Preserve ReturnedULongArrayOne(j)
                                    ReturnedULongArrayOne(j) = CULng(strULongRangeNew(0))
                                    ReDim Preserve ReturnedULongArrayTwo(j)
                                    ReturnedULongArrayTwo(j) = CULng(strULongRangeNew(1))
                                End If

                            End If


                        Else
                            MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                            ForcedExitSub = True
                            DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                            Exit Sub 'To avoid the Overall Numeric Check(s) AND the LOOP
                        End If

                    Else
                        MsgBox("""" & strInputLines(j) & """" & strLanguage_Typebox(19).Replace("{Line}", (j + 1).ToString) & vbCrLf & strLanguage_Typebox(17), MsgBoxStyle.Exclamation)    'This is not a valid numeric range format.
                        ForcedExitSub = True
                        DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                        Exit Sub 'To avoid the Overall Numeric Check AND the LOOP
                    End If
                Next

                If isOverallNumeric Then
                    'Text's Lines have been found all valid number-ranges, now the whole lines() will be returned
                    DialogResult = DialogResult.OK
                    Close()

                Else
                    MsgBox(strLanguage_Typebox(6) & vbCrLf & strLanguage_Typebox(7), MsgBoxStyle.Exclamation)    'Only numeric values allowed
                    DialogResult = DialogResult.None 'Form doesn't close - User can correct the text
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region


#Region "Date"
    Private Sub subDate_Value()
        If ValidDateFrom <> DefaultDate AndAlso ValidDateTo <> DefaultDate Then
            If dtFrom.Value >= ValidDateFrom AndAlso
                dtFrom.Value <= ValidDateTo Then
                DialogResult = DialogResult.OK
                Close()
            Else
                MsgBox(strLanguage_Typebox(14) & ValidDateFrom.ToString & strLanguage_Typebox(15) & ValidDateTo.ToString, MsgBoxStyle.Exclamation) 'The date must span between: <value> and <value>
                DialogResult = DialogResult.None
            End If

        ElseIf ValidDateFrom <> DefaultDate AndAlso ValidDateTo = DefaultDate Then
            If dtFrom.Value >= ValidDateFrom Then
                DialogResult = DialogResult.OK
                Close()
            Else
                MsgBox(strLanguage_Typebox(12) & ValidDateFrom.ToString, MsgBoxStyle.Exclamation) 'You must specify a date no earlier than:
                DialogResult = DialogResult.None
            End If

        ElseIf ValidDateFrom = DefaultDate AndAlso ValidDateTo <> DefaultDate Then
            If dtFrom.Value <= ValidDateTo Then
                DialogResult = DialogResult.OK
                Close()
            Else
                MsgBox(strLanguage_Typebox(13) & ValidDateTo.ToString, MsgBoxStyle.Exclamation) 'You must specify a date no later than:
                DialogResult = DialogResult.None
            End If

        Else 'Then we have no valid dates, so anything's acceptable
            DialogResult = DialogResult.OK
            Close()
        End If

    End Sub

    Private Sub subDate_Range()
        If ValidDateFrom <> DefaultDate AndAlso ValidDateTo <> DefaultDate Then
            'If dtFrom.Value >= Date.Parse(ValidDateFrom, DateFormat, CultureInfo.InvariantCulture) Then
            '    If dtTo.Value <= Date.Parse(ValidDateTo, DateFormat, CultureInfo.InvariantCulture) Then
            If dtFrom.Value >= ValidDateFrom Then
                If dtTo.Value <= ValidDateTo Then
                    DialogResult = DialogResult.OK
                    Close()

                Else
                    MsgBox(strLanguage_Typebox(13) & ValidDateTo.ToString, MsgBoxStyle.Exclamation) 'You must specify a date no later than:
                    DialogResult = DialogResult.None
                End If

            Else
                MsgBox(strLanguage_Typebox(12) & ValidDateFrom.ToString, MsgBoxStyle.Exclamation) 'You must specify a date no earlier than:
                DialogResult = DialogResult.None
            End If

        ElseIf ValidDateFrom <> DefaultDate AndAlso ValidDateTo = DefaultDate Then
            If dtFrom.Value >= ValidDateFrom Then
                DialogResult = DialogResult.OK
                Close()
            Else
                MsgBox(strLanguage_Typebox(12) & ValidDateFrom.ToString, MsgBoxStyle.Exclamation) 'You must specify a date no earlier than:
                DialogResult = DialogResult.None
            End If
        ElseIf ValidDateFrom = DefaultDate AndAlso ValidDateTo <> DefaultDate Then
            If dtTo.Value <= ValidDateTo Then
                DialogResult = DialogResult.OK
                Close()
            Else
                MsgBox(strLanguage_Typebox(13) & ValidDateTo.ToString, MsgBoxStyle.Exclamation) 'You must specify a date no later than:
                DialogResult = DialogResult.None
            End If
        Else
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub subDate_Array()
        Dim DatesList As New List(Of Date)

        For i = 0 To txtInput.Lines.Count - 1
            Dim CurDateResult As Date = DefaultDate

            If Date.TryParse(txtInput.Lines(i), CurDateResult) Then
                If ValidDateFrom <> DefaultDate AndAlso CurDateResult < ValidDateFrom Then
                    MsgBox(strLanguage_Typebox(44) & i & strLanguage_Typebox(45) & txtInput.Lines(i) & strLanguage_Typebox(46) & ValidDateFrom.ToString, MsgBoxStyle.Exclamation) 'Line (i) is outside the lower bound
                    DialogResult = DialogResult.None
                    Exit Sub
                End If
                If ValidDateTo <> DefaultDate AndAlso CurDateResult > ValidDateTo Then
                    MsgBox(strLanguage_Typebox(44) & i & strLanguage_Typebox(45) & txtInput.Lines(i) & strLanguage_Typebox(47) & ValidDateTo.ToString, MsgBoxStyle.Exclamation) 'Line (i) is outside the upper bound
                    DialogResult = DialogResult.None
                    Exit Sub
                End If

            Else
                MsgBox(strLanguage_Typebox(44) & i & strLanguage_Typebox(45) & txtInput.Lines(i) & strLanguage_Typebox(48), MsgBoxStyle.Exclamation) 'Line (i) cannot be converted to a date/time
                DialogResult = DialogResult.None
                Exit Sub
            End If

            DatesList.Add(CurDateResult)
        Next

        ReturnedDateArrayOne = DatesList.ToArray
        DialogResult = DialogResult.OK
        Close()
    End Sub

#End Region
#Region "ComboBox"
    Private Sub subComboBox()
        If cbIndexReturner.SelectedIndex <> -1 OrElse AllowNothingAsResult Then
            DialogResult = DialogResult.OK
            Close()
        Else
            MsgBox(strLanguage_Typebox(37) & vbCrLf & strLanguage_Typebox(2) & vbCrLf & RemCtrHotLetter(OK_Button) & strLanguage_Typebox(3), MsgBoxStyle.Exclamation) 'You need to select an item first!; If you want to cancel this operation, please push the x button
        End If
    End Sub
#End Region

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            If TypeBoxMode = TypeMode._Text Then
                If AllowNothingAsResult OrElse strUserInput.Length > 0 Then
                    If isNumericOnly Then 'Double
                        If Not MultiLine Then 'Double NonMultiline
                            If isRange Then Call subStrDouble_Range() Else Call subStrDouble_Value()

                        Else 'Double, MultiLine
                            If MaximumValidTextSize <> -1 AndAlso MaximumValidTextSize <> Double.MinValue AndAlso strUserInput.Length > MaximumValidTextSize Then
                                MsgBox(strLanguage_Typebox(10) & MaximumValidTextSize, MsgBoxStyle.Exclamation) 'Text's characters number must be lower than or equal to:
                                Exit Sub
                                txtInput.Select()

                            ElseIf MinimumValidTextSize <> -1 AndAlso MinimumValidTextSize <> Double.MinValue AndAlso strUserInput.Length < MinimumValidTextSize Then
                                MsgBox(strLanguage_Typebox(9) & MinimumValidTextSize, MsgBoxStyle.Exclamation)  'Text's characters number must be greater than or equal to:
                                DialogResult = DialogResult.None
                                txtInput.Select()

                            Else
                                If isRange Then 'Double Range Array
                                    Dim ForceExitSub As Boolean
                                    Call subStrDouble_RangeArray(ForceExitSub)
                                    If ForceExitSub Then Exit Sub

                                Else 'Double Value Array()
                                    Dim ForceExitSub As Boolean = False
                                    Call subStrDouble_ValueArray(ForceExitSub)
                                    If ForceExitSub Then Exit Sub
                                End If
                            End If
                        End If

                    Else
                        If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                            DialogResult = DialogResult.OK
                            Close()

                        Else
                            If MaximumValidTextSize <> -1 AndAlso MaximumValidTextSize <> Double.MinValue AndAlso strUserInput.Length > MaximumValidTextSize Then
                                MsgBox(strLanguage_Typebox(10) & MaximumValidTextSize, MsgBoxStyle.Exclamation) 'Text's characters number must be lower than or equal to:
                                Exit Sub
                                txtInput.Select()

                            ElseIf MinimumValidTextSize <> -1 AndAlso MinimumValidTextSize <> Double.MinValue AndAlso strUserInput.Length < MinimumValidTextSize Then
                                MsgBox(strLanguage_Typebox(9) & MinimumValidTextSize, MsgBoxStyle.Exclamation)  'Text's characters number must be greater than or equal to:
                                DialogResult = DialogResult.None
                                txtInput.Select()

                            Else
                                If Not MultiLine Then 'NonNumeric NonMultiline
                                    If MustntStartWthNum Then
                                        Call subText_String_CANTStartWithNum()
                                    Else 'No need to check if string is empty, because check is implemented before this call
                                        Call subText_String_CanStartWithNum()
                                    End If

                                Else
                                    Dim strInputLines() As String = strUserInputs
                                    Dim isNotNum1stChar As Boolean = True

                                    If Not MustntStartWthNum Then 'Text, MultiLine, CAN start with num
                                        Call subText_Array_CanStartWithNum()

                                    Else 'Text, MultiLine, and Mustn't start with num
                                        Call subText_Array_CANTStartWithNum()
                                    End If

                                End If
                            End If
                        End If

                    End If

                Else
                    MsgBox(strLanguage_Typebox(1) & vbCrLf & strLanguage_Typebox(2) & RemCtrHotLetter(Cancel_Button) & strLanguage_Typebox(3), MsgBoxStyle.Exclamation) 'The text cannot be left blank.
                    DialogResult = DialogResult.None
                    txtInput.Select()
                End If

            ElseIf TypeBoxMode = TypeMode._Double Then
                If AllowNothingAsResult OrElse strUserInput.Length > 0 Then
                    If Not MultiLine Then 'Double NonMultiline
                        If isRange Then Call subDouble_Range() Else Call subDouble_Value()

                    Else 'Double, MultiLine
                        If isRange Then 'Double Range Array
                            Dim ForceExitSub As Boolean
                            Call subDouble_RangeArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub

                        Else 'Double Value Array()
                            Dim ForceExitSub As Boolean = False
                            Call subDouble_ValueArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub
                        End If
                    End If

                Else
                    MsgBox(strLanguage_Typebox(1) & vbCrLf & strLanguage_Typebox(2) & RemCtrHotLetter(Cancel_Button) & strLanguage_Typebox(3), MsgBoxStyle.Exclamation) 'The text cannot be left blank.
                    DialogResult = DialogResult.None
                    txtInput.Select()
                End If

            ElseIf TypeBoxMode = TypeMode._Decimal Then
                If AllowNothingAsResult OrElse strUserInput.Length > 0 Then
                    If Not MultiLine Then 'decimal NonMultiline
                        If isRange Then Call subDecimal_Range() Else Call subDecimal_Value()

                    Else 'decimal, MultiLine
                        If isRange Then 'decimal Range Array
                            Dim ForceExitSub As Boolean
                            Call subDecimal_RangeArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub

                        Else 'decimal Value Array()
                            Dim ForceExitSub As Boolean = False
                            Call subDecimal_ValueArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub
                        End If
                    End If

                Else
                    MsgBox(strLanguage_Typebox(1) & vbCrLf & strLanguage_Typebox(2) & RemCtrHotLetter(Cancel_Button) & strLanguage_Typebox(3), MsgBoxStyle.Exclamation) 'The text cannot be left blank.
                    DialogResult = DialogResult.None
                    txtInput.Select()
                End If

            ElseIf TypeBoxMode = TypeMode._Single Then
                If AllowNothingAsResult OrElse strUserInput.Length > 0 Then
                    If Not MultiLine Then 'Single NonMultiline
                        If isRange Then Call subSingle_Range() Else Call subSingle_Value()

                    Else 'Single, MultiLine
                        If isRange Then 'Single Range Array
                            Dim ForceExitSub As Boolean
                            Call subSingle_RangeArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub

                        Else 'Single Value Array()
                            Dim ForceExitSub As Boolean = False
                            Call subSingle_ValueArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub
                        End If
                    End If

                Else
                    MsgBox(strLanguage_Typebox(1) & vbCrLf & strLanguage_Typebox(2) & RemCtrHotLetter(Cancel_Button) & strLanguage_Typebox(3), MsgBoxStyle.Exclamation) 'The text cannot be left blank.
                    DialogResult = DialogResult.None
                    txtInput.Select()
                End If

            ElseIf TypeBoxMode = TypeMode._Integer Then
                If AllowNothingAsResult OrElse strUserInput.Length > 0 Then
                    If Not MultiLine Then 'Integer NonMultiline
                        If isRange Then Call subInteger_Range() Else Call subInteger_Value()

                    Else 'Integer, MultiLine
                        If isRange Then 'Integer Range Array
                            Dim ForceExitSub As Boolean
                            Call subInteger_RangeArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub

                        Else 'Integer Value Array()
                            Dim ForceExitSub As Boolean = False
                            Call subInteger_ValueArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub
                        End If
                    End If

                Else
                    MsgBox(strLanguage_Typebox(1) & vbCrLf & strLanguage_Typebox(2) & RemCtrHotLetter(Cancel_Button) & strLanguage_Typebox(3), MsgBoxStyle.Exclamation) 'The text cannot be left blank.
                    DialogResult = DialogResult.None
                    txtInput.Select()
                End If

            ElseIf TypeBoxMode = TypeMode._UInteger Then
                If AllowNothingAsResult OrElse strUserInput.Length > 0 Then
                    If Not MultiLine Then 'UInteger NonMultiline
                        If isRange Then Call subUInteger_Range() Else Call subUInteger_Value()

                    Else 'UInteger, MultiLine
                        If isRange Then 'UInteger Range Array
                            Dim ForceExitSub As Boolean
                            Call subUInteger_RangeArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub

                        Else 'UInteger Value Array()
                            Dim ForceExitSub As Boolean = False
                            Call subUInteger_ValueArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub
                        End If
                    End If

                Else
                    MsgBox(strLanguage_Typebox(1) & vbCrLf & strLanguage_Typebox(2) & RemCtrHotLetter(Cancel_Button) & strLanguage_Typebox(3), MsgBoxStyle.Exclamation) 'The text cannot be left blank.
                    DialogResult = DialogResult.None
                    txtInput.Select()
                End If

            ElseIf TypeBoxMode = TypeMode._Short Then
                If AllowNothingAsResult OrElse strUserInput.Length > 0 Then
                    If Not MultiLine Then 'Short NonMultiline
                        If isRange Then Call subShort_Range() Else Call subShort_Value()

                    Else 'Short, MultiLine
                        If isRange Then 'Short Range Array
                            Dim ForceExitSub As Boolean
                            Call subShort_RangeArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub

                        Else 'Short Value Array()
                            Dim ForceExitSub As Boolean = False
                            Call subShort_ValueArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub
                        End If

                    End If

                Else
                    MsgBox(strLanguage_Typebox(1) & vbCrLf & strLanguage_Typebox(2) & RemCtrHotLetter(Cancel_Button) & strLanguage_Typebox(3), MsgBoxStyle.Exclamation) 'The text cannot be left blank.
                    DialogResult = DialogResult.None
                    txtInput.Select()
                End If

            ElseIf TypeBoxMode = TypeMode._UShort Then
                If AllowNothingAsResult OrElse strUserInput.Length > 0 Then
                    If Not MultiLine Then 'UShort NonMultiline
                        If isRange Then Call subUShort_Range() Else Call subUShort_Value()

                    Else 'UShort, MultiLine
                        If isRange Then 'UShort Range Array
                            Dim ForceExitSub As Boolean
                            Call UShort_RangeArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub

                        Else 'UShort Value Array()
                            Dim ForceExitSub As Boolean = False
                            Call subUShort_ValueArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub
                        End If
                    End If

                Else
                    MsgBox(strLanguage_Typebox(1) & vbCrLf & strLanguage_Typebox(2) & RemCtrHotLetter(Cancel_Button) & strLanguage_Typebox(3), MsgBoxStyle.Exclamation) 'The text cannot be left blank.
                    DialogResult = DialogResult.None
                    txtInput.Select()
                End If

            ElseIf TypeBoxMode = TypeMode._Long Then
                If AllowNothingAsResult OrElse strUserInput.Length > 0 Then
                    If Not MultiLine Then 'Long NonMultiline
                        If isRange Then Call subLong_Range() Else Call subLong_Value()

                    Else 'Long, MultiLine
                        If isRange Then 'Long Range Array
                            Dim ForceExitSub As Boolean
                            Call Long_RangeArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub

                        Else 'Long Value Array()
                            Dim ForceExitSub As Boolean = False
                            Call subLong_ValueArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub
                        End If
                    End If

                Else
                    MsgBox(strLanguage_Typebox(1) & vbCrLf & strLanguage_Typebox(2) & RemCtrHotLetter(Cancel_Button) & strLanguage_Typebox(3), MsgBoxStyle.Exclamation) 'The text cannot be left blank.
                    DialogResult = DialogResult.None
                    txtInput.Select()
                End If

            ElseIf TypeBoxMode = TypeMode._ULong Then
                If AllowNothingAsResult OrElse strUserInput.Length > 0 Then
                    If Not MultiLine Then 'ULong NonMultiline
                        If isRange Then Call subULong_Range() Else Call subULong_Value()

                    Else 'ULong, MultiLine
                        If isRange Then 'ULong Range Array
                            Dim ForceExitSub As Boolean
                            Call subULong_RangeArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub

                        Else 'ULong Value Array()
                            Dim ForceExitSub As Boolean = False
                            Call subULong_ValueArray(ForceExitSub)
                            If ForceExitSub Then Exit Sub
                        End If

                    End If

                Else
                    MsgBox(strLanguage_Typebox(1) & vbCrLf & strLanguage_Typebox(2) & RemCtrHotLetter(Cancel_Button) & strLanguage_Typebox(3), MsgBoxStyle.Exclamation) 'The text cannot be left blank.
                    DialogResult = DialogResult.None
                    txtInput.Select()
                End If

            ElseIf TypeBoxMode = TypeMode._Date Then
                If MultiLine = True Then

                    If AllowNothingAsResult AndAlso txtInput.Text = "" Then
                        DialogResult = DialogResult.OK
                        Close()
                    ElseIf txtInput.Text <> "" Then
                        Call subDate_Array()
                    Else
                        MsgBox(strLanguage_Typebox(43), MsgBoxStyle.Exclamation) 'You must specify at least one date
                    End If

                Else
                    'If it is a single date there are four options, we either have no valid from or To date, so anything is accepted, or one of the three combinations
                    If Not isRange Then
                        If AllowNothingAsResult AndAlso dtFrom.Value = DefaultDate Then
                            DialogResult = DialogResult.OK
                            Close()
                        ElseIf dtFrom.Value <> DefaultDate Then
                            Call subDate_Value()
                        Else
                            MsgBox(strLanguage_Typebox(38), MsgBoxStyle.Exclamation) 'You must select a date first!
                            dtFrom.Select()
                        End If

                    Else
                        If AllowNothingAsResult AndAlso dtFrom.Value = DefaultDate AndAlso dtTo.Value = DefaultDate Then
                            DialogResult = DialogResult.OK
                            Close()
                        ElseIf (Not WholeRangeMustChange AndAlso (dtFrom.Value <> DefaultDate OrElse dtTo.Value <> DefaultDate)) OrElse
                        (WholeRangeMustChange AndAlso dtFrom.Value <> DefaultDate AndAlso dtTo.Value <> DefaultDate) Then
                            Call subDate_Range()
                        Else
                            MsgBox(strLanguage_Typebox(38), MsgBoxStyle.Exclamation) 'You must select a date first!
                            dtFrom.Select()
                        End If
                    End If
                End If

            ElseIf TypeBoxMode = TypeMode._ComboBox Then
                Call subComboBox()
            End If

        Catch ex As Exception
            TopMost = False
            CreateCrashFile(ex, True)
            DialogResult = DialogResult.Cancel
        End Try

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub dtFrom_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtFrom.ValueChanged
        If dtFrom.Value > dtTo.Value Then
            dtTo.Value = dtFrom.Value.Date
            'If DateFormat.Contains("HH") AndAlso dtFrom.Value.Hour = 0 Then
            '    dtTo.Value = dtTo.Value.AddHours(23)
            'End If
            'If DateFormat.Contains("mm") AndAlso dtFrom.Value.Minute = 0 Then
            '    dtTo.Value = dtTo.Value.AddMinutes(59)
            'End If
            'If DateFormat.Contains("ss") AndAlso dtFrom.Value.Second = 0 Then
            '    dtTo.Value = dtTo.Value.AddSeconds(59)
            'End If
        End If
    End Sub

    Private Sub dtTo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtTo.ValueChanged
        'Dim TimeDifference As TimeSpan
        If dtTo.Value < dtFrom.Value Then
            dtFrom.Value = dtTo.Value
            'If DateFormat.Contains("HH") Then 'AndAlso dtTo.Value.Hour = 23 Then
            '    TimeDifference = TimeSpan.FromHours(dtTo.Value.Hour)
            '    dtFrom.Value = dtFrom.Value.Subtract(TimeDifference)
            'End If
            'If DateFormat.Contains("mm") Then 'AndAlso dtTo.Value.Minute = 59 Then
            '    TimeDifference = TimeSpan.FromMinutes(dtTo.Value.Minute)
            '    dtFrom.Value = dtFrom.Value.Subtract(TimeDifference)
            'End If
            'If DateFormat.Contains("ss") Then 'AndAlso dtTo.Value.Second = 59 Then
            '    TimeDifference = TimeSpan.FromSeconds(dtTo.Value.Second)
            '    dtFrom.Value = dtFrom.Value.Subtract(TimeDifference)
            'End If
        End If
    End Sub

    Public Shadows Sub Show() 'This form should only be called as a dialogue!
        Throw New InvalidOperationException("Use ShowDialog to display this dialogue")
    End Sub

    Private Sub lblInfo_SizeChanged(sender As Object, e As System.EventArgs) Handles lblInfo.SizeChanged, lblInfo.TextChanged
        Dim ProposedWidth As Integer

        If lblInfo.Width + lblInfo.Location.X > Width Then
            ProposedWidth = lblInfo.Width + lblInfo.Location.X + (Width - (tlpButtons.Location.X + tlpButtons.Width))

            If ProposedWidth > My.Computer.Screen.Bounds.Width Then
                Width = My.Computer.Screen.Bounds.Width
            Else
                Width = ProposedWidth
            End If
        End If

    End Sub

    Private Sub lblValidRange_SizeChanged(sender As Object, e As System.EventArgs) Handles lblValidRange.SizeChanged
        Dim ProposedWidth As Integer

        If lblValidRange.Width + lblValidRange.Location.X > tlpButtons.Location.X - 8 Then
            ProposedWidth = lblValidRange.Width + lblValidRange.Location.X + 8 + tlpButtons.Width + (Width - (tlpButtons.Location.X + tlpButtons.Width))

            If ProposedWidth > My.Computer.Screen.Bounds.Width Then
                Width = My.Computer.Screen.Bounds.Width
            Else
                Width = ProposedWidth
            End If
        End If
    End Sub

    Private Sub lblMaxLength_SizeChanged(sender As Object, e As System.EventArgs) Handles lblPerLineBounds.SizeChanged
        Dim ProposedWidth As Integer

        If lblPerLineBounds.Width + lblPerLineBounds.Location.X > tlpButtons.Location.X - 8 Then
            ProposedWidth = lblPerLineBounds.Width + lblPerLineBounds.Location.X + 8 + tlpButtons.Width + (Width - (tlpButtons.Location.X + tlpButtons.Width))

            If ProposedWidth > My.Computer.Screen.Bounds.Width Then
                Width = My.Computer.Screen.Bounds.Width
            Else
                Width = ProposedWidth
            End If
        End If
    End Sub

    Private Sub txtInput_KeyDown(sender As Object, e As KeyEventArgs) Handles txtInput.KeyDown
        If e.KeyCode = Keys.Enter AndAlso (e.Modifiers = Keys.Shift OrElse e.Modifiers = Keys.Control) Then Call OK_Button_Click(Nothing, Nothing)
    End Sub

    Private Sub txtInput_TextChanged(sender As Object, e As EventArgs) Handles txtInput.TextChanged
        strUserInput = txtInput.Text
        If txtInput.Multiline Then strUserInputs = txtInput.Lines
    End Sub

End Class
