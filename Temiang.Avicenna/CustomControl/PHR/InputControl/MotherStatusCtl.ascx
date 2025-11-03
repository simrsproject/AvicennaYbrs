<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MotherStatusCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.MotherStatusCtl" %>

<fieldset>
    <legend>Mother's Status</legend>
    <table width="100%">
        <tr>
            <td class="label">Pregnant</td>
            <td class="entry">
                <telerik:RadCheckBox runat="server" ID="chkIsPregnant"></telerik:RadCheckBox>
            </td>
            <td style="width: 20%;"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Gestational Age (Month)</td>
            <td class="entry">
                <telerik:RadNumericTextBox runat="server" ID="txtGestationalAge" NumberFormat-DecimalDigits="0" Width="50px"></telerik:RadNumericTextBox>
            </td>
            <td style="width: 20%;"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Breast Feeding</td>
            <td class="entry">
                <telerik:RadCheckBox runat="server" ID="chkIsBreastFeeding"></telerik:RadCheckBox>
            </td>
            <td style="width: 20%;"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Age Of Baby</td>
            <td class="entry">
                <telerik:RadNumericTextBox runat="server" ID="txtAgeOfBabyInYear" NumberFormat-DecimalDigits="0" Width="50px"/>&nbsp;Year&nbsp;/
                <telerik:RadNumericTextBox runat="server" ID="txtAgeOfBabyInMonth" NumberFormat-DecimalDigits="0" Width="50px"/>&nbsp;Month&nbsp;/
                <telerik:RadNumericTextBox runat="server" ID="txtAgeOfBabyInDay" NumberFormat-DecimalDigits="0" Width="50px"/>&nbsp;Day
            </td>
            <td style="width: 20%;"></td>
            <td></td>
        </tr>
    </table>
</fieldset>
