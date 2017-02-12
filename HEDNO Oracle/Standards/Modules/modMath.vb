'Version 1.6.1 2016-07-05
'Added SimplifyArray and isNumericValue for check is Simplify
'Added NChooseK Combinations
Option Strict On

Imports System.Math
Imports MathParserNet
Imports System.Threading.Thread
Imports System.Globalization

Module modMath
    Public MathEvaluator As New Parser

    Public lstVariablesNames As New List(Of String)
    Public lstVariablesValues As New List(Of String)

    Public lstFunctionsNames As New List(Of String)
    Public lstFunctionsValues As New List(Of String)

    Public EquationModeOn As Boolean
    Public AddVariableModeOn As Boolean
    Public AddFunctionModeOn As Boolean

    Public Function Hypergeometric_Distribution(ByVal ν As Integer, ByVal k As Integer, ByVal N As Integer, ByVal r As Integer) As Double
        Return (NChooseK(ν, k) * NChooseK(N - ν, r - k)) / NChooseK(N, r)
    End Function

    Public Function NChooseK(ByVal N As Integer, ByVal K As Integer) As Double
        Dim result As Double = 1

        For i As Integer = 1 To K
            result *= N - (K - i)
            result /= i
        Next i

        Return result
    End Function

    Public Function nChooseK(Of T)(ByVal Values As List(Of T), ByVal k As Integer, Optional ByRef Result As List(Of List(Of T)) = Nothing, Optional ByRef CurCombination As List(Of T) = Nothing, Optional ByVal Offset As Integer = 0) As List(Of List(Of T))
        Dim n = Values.Count
        If CurCombination Is Nothing Then CurCombination = New List(Of T)
        If Result Is Nothing Then Result = New List(Of List(Of T))

        If k <= 0 Then
            Result.Add(CurCombination.ToArray.ToList)
            Return Result
        Else
            For i = Offset To n - k
                CurCombination.Add(Values(i))
                nChooseK(Values, k - 1, Result, CurCombination, i + 1)
                CurCombination.RemoveAt(CurCombination.Count - 1)
            Next

            Return Result
        End If

    End Function

    Public Function Mean(ByVal Numbers As Double()) As Double
        Return (Aggregate Num In Numbers Into Average())
    End Function
    Public Function Mean(ByVal Numbers As Double()()) As List(Of Double)
        Dim lstResult As New List(Of Double)

        For Each Num In Numbers
            lstResult.Add(Mean(Num))
        Next

        Return lstResult
    End Function

    Public Function Median(ByVal Numbers As Double()) As Double
        Array.Sort(Numbers)

        If Numbers.Length Mod 2 = 0 Then
            Return (Numbers(CInt((Numbers.Length / 2) - 1)) + Numbers(CInt((Numbers.Length / 2)))) / 2
        Else
            Return Numbers(CInt((Numbers.Length - 1) / 2))
        End If

    End Function
    Public Function Median(ByVal Numbers As Double()()) As List(Of Double)
        Dim lstResult As New List(Of Double)

        For Each Num In Numbers
            lstResult.Add(Median(Num))
        Next

        Return lstResult
    End Function

#Region "NormalRound"
    Public Function NormalRound(Of T)(ByVal Num As T, Optional intRound As UInteger = 3, Optional MinTotalLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False) As String
        Dim Result As String = String.Empty

        If IsNumeric(Num) Then
            Dim ProperNum As String = Num.ToString.Replace(".", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)
            Dim ResultStr As String = Zero_A_Num(Round(CDbl(ProperNum), CInt(intRound)), CInt(intRound)) 'String.Format("{0:n" & intRound & "}", CDbl(ProperNum))

            If CDbl(ResultStr) = 0 AndAlso CDbl(ProperNum) <> 0 Then BecameZeroByRounding = True

            If MinTotalLength = -1 Then
                Result = ResultStr
            Else
                Result = Zero_A_Num(ResultStr, MinTotalLength, , True)
            End If

        Else
            Result = Num.ToString
        End If

        Return Result
    End Function
    Public Function NormalRound(Of T)(ByVal Nums() As T, Optional intRound As UInteger = 3, Optional MinTotalLength As Integer = -1, Optional ByRef BecameZeroByRounding() As Boolean = Nothing, Optional ByRef AtLeastOneBecomeZeroByRounding As Boolean = False) As String()
        Dim Result As New List(Of String)
        Dim lstBecameZeroByRounding As New List(Of Boolean)

        For Each Num In Nums
            lstBecameZeroByRounding.Add(False)
            Result.Add(NormalRound(Num, intRound, MinTotalLength, lstBecameZeroByRounding.Item(lstBecameZeroByRounding.Count - 1)))
        Next

        BecameZeroByRounding = lstBecameZeroByRounding.ToArray
        AtLeastOneBecomeZeroByRounding = (From Bool In lstBecameZeroByRounding Order By Bool Descending).FirstOrDefault

        Return Result.ToArray
    End Function
    Public Function NormalRound(Of T)(ByVal Nums As List(Of T), Optional intRound As UInteger = 3, Optional MinTotalLength As Integer = -1, Optional ByRef BecameZeroByRounding As List(Of Boolean) = Nothing, Optional ByRef AtLeastOneBecomeZeroByRounding As Boolean = False) As List(Of String)
        Dim Result As New List(Of String)
        Dim lstBecameZeroByRounding As New List(Of Boolean)

        For Each Num In Nums
            lstBecameZeroByRounding.Add(False)
            Result.Add(NormalRound(Num, intRound, MinTotalLength, lstBecameZeroByRounding.Item(lstBecameZeroByRounding.Count - 1)))
        Next

        BecameZeroByRounding = lstBecameZeroByRounding
        AtLeastOneBecomeZeroByRounding = (From Bool In lstBecameZeroByRounding Order By Bool Descending).FirstOrDefault

        Return Result
    End Function
#End Region

#Region "StatRound"
    Public Function StatRound(Of T)(ByVal Num As T, Optional intRound As UInteger = 3, Optional MinTotalLength As Integer = -1, Optional ByRef BecameZeroByRounding As Boolean = False) As String
        Dim Result As String = String.Empty

        If IsNumeric(Num) Then
            Dim ProperNum As String = Num.ToString.Replace(".", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)
            Dim ResultStr As String = Zero_A_Num(Round(CDbl(ProperNum), CInt(intRound)), CInt(intRound)) 'String.Format("{0:n" & intRound & "}", CDbl(ProperNum))

            If CDbl(ResultStr) <> 0 OrElse CDbl(ProperNum) = 0 Then
                BecameZeroByRounding = False
                If MinTotalLength = -1 Then
                    Result = ResultStr
                Else
                    Result = Zero_A_Num(ResultStr, MinTotalLength)
                End If

            Else
                BecameZeroByRounding = True
                If intRound = 0 Then
                    Result = "<1"
                ElseIf intRound = 1 Then
                    Result = "<0.1"
                Else
                    Result = "<0." & Zero_A_Num(0, CInt(intRound - 1)) & "1"
                End If

                If Result.Length < MinTotalLength Then Result = SpaceAString(Result, MinTotalLength)
            End If

        Else
            Result = Num.ToString
        End If

        Return Result
    End Function
    Public Function StatRound(Of T)(ByVal Nums() As T, Optional intRound As UInteger = 3, Optional MinTotalLength As Integer = -1, Optional ByRef BecameZeroByRounding() As Boolean = Nothing, Optional ByRef AtLeastOneBecomeZeroByRounding As Boolean = False) As String()
        Dim Result As New List(Of String)
        Dim lstBecameZeroByRounding As New List(Of Boolean)

        For Each Num In Nums
            Dim BecameZeroByRoundingNow As Boolean
            Result.Add(StatRound(Num, intRound, MinTotalLength, BecameZeroByRoundingNow))
            lstBecameZeroByRounding.Add(BecameZeroByRoundingNow)
        Next

        BecameZeroByRounding = lstBecameZeroByRounding.ToArray
        AtLeastOneBecomeZeroByRounding = (From Bool In lstBecameZeroByRounding Order By Bool Descending).FirstOrDefault

        Return Result.ToArray
    End Function
    Public Function StatRound(Of T)(ByVal Nums As List(Of T), Optional intRound As UInteger = 3, Optional MinTotalLength As Integer = -1, Optional ByRef BecameZeroByRounding As List(Of Boolean) = Nothing, Optional ByRef AtLeastOneBecomeZeroByRounding As Boolean = False) As List(Of String)
        Dim Result As New List(Of String)
        Dim lstBecameZeroByRounding As New List(Of Boolean)

        For Each Num In Nums
            Dim BecameZeroByRoundingNow As Boolean
            Result.Add(StatRound(Num, intRound, MinTotalLength, BecameZeroByRoundingNow))
            lstBecameZeroByRounding.Add(BecameZeroByRoundingNow)
        Next

        BecameZeroByRounding = lstBecameZeroByRounding
        AtLeastOneBecomeZeroByRounding = (From Bool In lstBecameZeroByRounding Order By Bool Descending).FirstOrDefault

        Return Result
    End Function
#End Region

#Region "Math Parser"
#Region "Add Variable(s)"
    Public Sub AddVariable(ByVal VariableName As String, ByVal VariableValue As String, Optional ByVal UpdateSettings As Boolean = True)
        If Not isContained(VariableName, lstFunctionsNames, False, True) AndAlso Not isContained(VariableName, lstVariablesNames, False, True) Then

            Dim CurCulture As String = CurrentThread.CurrentCulture.Name
            Dim CurCultureUI As String = CurrentThread.CurrentUICulture.Name
            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")

            MathEvaluator.AddVariable(VariableName, (VariableValue.Replace(",", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)))
            lstVariablesNames.Add(VariableName)
            lstVariablesValues.Add(SimplifyValue(VariableValue.Replace(",", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)))

            If UpdateSettings Then
                Dim EqualsList As New List(Of String)
                For Each Item In lstVariablesNames
                    EqualsList.Add("=")
                Next

                'Dim VariablesNew As List(Of String) = (From Var In lstVariablesValues Select Var.Replace(CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")).ToList

                strSettings(34) = "034Variables=" & CreateListString(lstVariablesNames, EqualsList, lstVariablesValues)
                WriteSettings(strSettings, "AddVariable(s)")
            End If

            CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CurCulture)
            CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(CurCultureUI)
        End If
    End Sub
    Public Sub AddVariable(ByVal EquationString As String, Optional UpdateSettings As Boolean = True)
        If EquationString <> "" Then
            Dim IndexOfEqualSign As Integer = Count("="c, EquationString)
            Dim VariableNameAndValue() As String = EquationString.Split("="c)

            AddVariable(VariableNameAndValue(0), VariableNameAndValue(1), UpdateSettings)
        End If
    End Sub
    Public Sub AddVariables(ByVal EquationStrings() As String, Optional UpdateSettings As Boolean = True)
        For Each EquationString In EquationStrings
            Call AddVariable(EquationString, UpdateSettings)
        Next
    End Sub
#End Region

#Region "Add Function(s)"
    Public Sub AddFunction(ByVal FunctionName As String, ByVal FunctionArguments As MathParserNet.FunctionArgumentList, ByVal FunctionValue As String, Optional UpdateSettings As Boolean = True)
        Dim CurCulture As String = CurrentThread.CurrentCulture.Name
        Dim CurCultureUI As String = CurrentThread.CurrentUICulture.Name
        CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
        CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")

        MathEvaluator.AddFunction(FunctionName, FunctionArguments, FunctionValue.Replace(",", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator))
        lstFunctionsNames.Add("func" & FunctionName & "{" & ArrayBox(False, ",", 0, True, FunctionArguments.ToArray) & "}")
        lstFunctionsValues.Add(FunctionValue.Replace(",", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator))

        If UpdateSettings = True Then
            Dim EqualsList As New List(Of String)
            For Each Item In lstFunctionsNames
                EqualsList.Add("=")
            Next

            Dim lstFunctionsNamesWithoutfunc As New List(Of String)
            lstFunctionsNamesWithoutfunc.AddRange(lstFunctionsNames)
            For i = 0 To lstFunctionsNamesWithoutfunc.Count - 1
                lstFunctionsNamesWithoutfunc.Item(i) = lstFunctionsNamesWithoutfunc.Item(i).Substring("func".Length)
            Next

            'Dim VariablesNew As List(Of String) = (From Var In lstFunctionsValues Select Var.Replace(CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")).ToList

            strSettings(35) = "035Functions=" & CreateListString(lstFunctionsNamesWithoutfunc, EqualsList, lstFunctionsValues)
            WriteSettings(strSettings, "AddVariable(s)")
        End If

        CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CurCulture)
        CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(CurCultureUI)

    End Sub
    Public Sub AddFunction(ByVal FunctionName As String, ByVal FunctionArguments() As String, ByVal FunctionValue As String, Optional UpdateSettings As Boolean = True)
        Dim ArgumentVariables As New MathParserNet.FunctionArgumentList
        ArgumentVariables.AddRange(FunctionArguments)
        Call AddFunction(FunctionName, ArgumentVariables, FunctionValue, UpdateSettings)
    End Sub
    Public Sub AddFunction(ByVal FunctionName As String, ByVal FunctionArguments As List(Of String), ByVal FunctionValue As String, Optional UpdateSettings As Boolean = True)
        Dim ArgumentVariables As New MathParserNet.FunctionArgumentList
        ArgumentVariables.AddRange(FunctionArguments)
        Call AddFunction(FunctionName, ArgumentVariables, FunctionValue, UpdateSettings)
    End Sub

    Public Sub AddFunction(ByVal FunctionString As String, Optional UpdateSettings As Boolean = True)
        If FunctionString <> "" Then
            Dim FunctionNameAndValue() As String = FunctionString.Split("="c)
            FunctionNameAndValue(0) = FunctionNameAndValue(0).Replace("(", "{").Replace(")", "}")
            FunctionNameAndValue(1) = FunctionNameAndValue(1).Replace("{", "(").Replace("}", ")").Replace("[", "(").Replace("]", ")")

            Dim IndexOfBracket As Integer = FunctionNameAndValue(0).IndexOf("{")

            Dim FunctionName As String = GetSubStr(FunctionNameAndValue(0), FunctionNameAndValue(0).IndexOf("{"))
            Dim Vars As List(Of String) = ReadList(FunctionNameAndValue(0).Substring(FunctionNameAndValue(0).IndexOf("{")), True, True)
            Dim FunctionValue As String = FunctionNameAndValue(1)

            Call AddFunction(FunctionName, Vars, FunctionValue, UpdateSettings)
        End If
    End Sub

    Public Sub AddFunctions(ByVal FunctionStrings() As String, Optional UpdateSettings As Boolean = True)
        For Each FunctionString In FunctionStrings
            Call AddFunction(FunctionString, UpdateSettings)
        Next
    End Sub

#End Region

    Public Sub RemoveAllMathVariables(Optional UpdateSettings As Boolean = True)
        MathEvaluator.RemoveAllVariables()
        lstVariablesNames.Clear()
        lstVariablesValues.Clear()

        If UpdateSettings Then
            strSettings(34) = "034Variables={}"
            WriteSettings(strSettings, "RemoveAllMathVariables")
        End If
    End Sub

    Public Sub RemoveAllFunctions(Optional UpdateSettings As Boolean = True)
        MathEvaluator.RemoveAllFunctions()
        lstFunctionsNames.Clear()
        lstFunctionsValues.Clear()

        If UpdateSettings Then
            strSettings(35) = "035Functions={}"
            WriteSettings(strSettings, "RemoveAllFunctions")
        End If
    End Sub

    Public Sub RemoveVariable(ByVal VariableName As String, ByVal IndexOfVariable As Integer, Optional UpdateSettings As Boolean = True)
        MathEvaluator.RemoveVariable(VariableName)
        lstVariablesNames.RemoveAt(IndexOfVariable)
        lstVariablesValues.RemoveAt(IndexOfVariable)

        If UpdateSettings Then
            Dim EqualsList As New List(Of String)
            For Each Item In lstVariablesNames
                EqualsList.Add("=")
            Next
            strSettings(34) = "034Variables=" & CreateListString(lstVariablesNames, EqualsList, lstVariablesValues)
            WriteSettings(strSettings, "AddVariable(s)")
        End If

    End Sub

    Public Sub RemoveFunction(ByVal FunctionName As String, ByVal IndexOfFunctions As Integer, Optional UpdateSettings As Boolean = True)
        MathEvaluator.RemoveFunction(FunctionName)
        lstFunctionsNames.RemoveAt(IndexOfFunctions)
        lstFunctionsValues.RemoveAt(IndexOfFunctions)

        If UpdateSettings Then
            Dim lstFunctionsNamesWithoutfunc As New List(Of String)
            lstFunctionsNamesWithoutfunc.AddRange(lstFunctionsNames)
            For i = 0 To lstFunctionsNamesWithoutfunc.Count - 1
                lstFunctionsNamesWithoutfunc.Item(i) = lstFunctionsNamesWithoutfunc.Item(i).Substring("func".Length)
            Next

            Dim EqualsList As New List(Of String)
            For Each Item In lstVariablesNames
                EqualsList.Add("=")
            Next
            strSettings(35) = "035Functions=" & CreateListString(lstFunctionsNamesWithoutfunc, EqualsList, lstFunctionsValues)
            WriteSettings(strSettings, "AddVariable(s)")
        End If

    End Sub

    Public Function SimplifyValue(Of T)(ByVal PossibleNum As T, Optional ByRef Succeeded As Boolean = True) As String
        'Succeeded as Optional in this case, because Unsuccessful result is the Original Value
        Dim Result As String = String.Empty
        Succeeded = True

        If IsNumeric(PossibleNum.ToString) Then
            Result = PossibleNum.ToString

        Else
            Try
                Dim CurCulture As String = CurrentThread.CurrentCulture.Name
                Dim CurCultureUI As String = CurrentThread.CurrentUICulture.Name
                CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
                CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")

                Result = MathEvaluator.SimplifyObject(PossibleNum.ToString.Replace(",", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)).ToString

                CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CurCulture)
                CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(CurCultureUI)
            Catch ex As Exception
                Succeeded = False
                Try
                    Result = PossibleNum.ToString
                Catch exc As Exception
                End Try
            End Try
        End If

        Return Result.Replace(CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")
    End Function

#Region "SimplyfyArray"
    'Succeeded is NON optional in this case, because unsuccessful result returns NaN in the list index of the error

    Public Function SimplifyArray(Of T)(ByVal Array As List(Of T), ByRef Succeeded As Boolean) As List(Of String)
        Dim Result As New List(Of String)
        Succeeded = True

        Dim CurCulture As String = CurrentThread.CurrentCulture.Name
        Dim CurCultureUI As String = CurrentThread.CurrentUICulture.Name
        CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
        CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
        For Each PossibleNum In Array
            If IsNumeric(PossibleNum.ToString) Then
                Result.Add(PossibleNum.ToString)

            Else
                Try
                    Result.Add(MathEvaluator.SimplifyObject(PossibleNum.ToString.Replace(",", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)).ToString)
                Catch ex As Exception
                    Succeeded = False
                    Result.Add(Double.NaN.ToString)
                End Try
            End If
        Next
        CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CurCulture)
        CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(CurCultureUI)

        Return Result
    End Function
    Public Function SimplifyArray(Of T)(ByVal Array() As T, ByRef Succeeded_IsOverallNumeric As Boolean) As String()
        Dim Result As New List(Of String)
        Succeeded_IsOverallNumeric = True

        Dim CurCulture As String = CurrentThread.CurrentCulture.Name
        Dim CurCultureUI As String = CurrentThread.CurrentUICulture.Name
        CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB")
        CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB")
        For Each PossibleNum In Array
            Try
                Result.Add(MathEvaluator.SimplifyObject(PossibleNum.ToString.Replace(",", CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)).ToString)
            Catch ex As Exception
                Succeeded_IsOverallNumeric = False
                Result.Add(Double.NaN.ToString)
            End Try
        Next
        CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CurCulture)
        CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(CurCultureUI)

        Return Result.ToArray
    End Function
#End Region

#End Region

End Module
