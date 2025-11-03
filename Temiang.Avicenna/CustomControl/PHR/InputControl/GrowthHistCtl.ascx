<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GrowthHistCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.GrowthHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<fieldset style="width: 49%;">
    <legend><b>GROWING HISTORY</b></legend>
    <table width="100%">

        <tr>
            <td class="label">Child number</td>
            <td colspan="2">
                <telerik:RadNumericTextBox NumberFormat-DecimalDigits="0" ID="txtChildNumber" runat="server" Width="50px" />
                &nbsp;from&nbsp;<telerik:RadNumericTextBox NumberFormat-DecimalDigits="0" ID="txtChildNumberFrom" runat="server" Width="50px" />
            </td>

        </tr>
        <tr>
            <td class="label">Birth head circumference</td>
            <td>
                <telerik:RadNumericTextBox ID="txtHeadCircum" NumberFormat-DecimalDigits="0" runat="server" Width="50px" />
            </td>
            <td>cm</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Birth weight</td>
            <td>
                <telerik:RadNumericTextBox NumberFormat-DecimalDigits="2" ID="txtWeight" runat="server" Width="50px" />
            </td>
            <td>gr</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Birth length</td>
            <td>
                <telerik:RadNumericTextBox NumberFormat-DecimalDigits="0" ID="txtLength" runat="server" Width="50px" />
            </td>
            <td>cm</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">ASI to age</td>
            <td style="width: 30px">
                <telerik:RadNumericTextBox ID="txtAsiToMonthAge" NumberFormat-DecimalDigits="0" runat="server" Width="50px" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Formula milk start age</td>
            <td style="width: 30px">
                <telerik:RadNumericTextBox ID="txtFormulaMilkStartAge" NumberFormat-DecimalDigits="0" runat="server" Width="50px" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Additional food start age</td>
            <td style="width: 30px">
                <telerik:RadNumericTextBox ID="txtAddFoodStartAge" NumberFormat-DecimalDigits="0" runat="server" Width="50px" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Current Diet</td>
            <td colspan="4">
                <telerik:RadTextBox ID="txtCurrentDiet" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Raise the head</td>
            <td>
                <telerik:RadNumericTextBox ID="txtRaiseHead" runat="server" NumberFormat-DecimalDigits="0" Width="50px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">On his stomach</td>
            <td>
                <telerik:RadNumericTextBox ID="txtProneAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="50px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Sitdown</td>
            <td>
                <telerik:RadNumericTextBox ID="txtSitAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="50px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Crawl</td>
            <td>
                <telerik:RadNumericTextBox ID="txtCrawlAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="50px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>

        </tr>
        <tr>
            <td class="label">Stand up</td>
            <td>
                <telerik:RadNumericTextBox ID="txtStandUpAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="50px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>

        </tr>
        <tr>
            <td class="label">Walk</td>
            <td>
                <telerik:RadNumericTextBox ID="txtWalkAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="50px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Grabbing objects</td>
            <td>
                <telerik:RadNumericTextBox ID="txtGrabbing" runat="server" NumberFormat-DecimalDigits="0" Width="50px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Holding objects</td>
            <td>
                <telerik:RadNumericTextBox ID="txtHolding" runat="server" NumberFormat-DecimalDigits="0" Width="50px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>

        <tr>
            <td class="label">Speak 3 - 6 word clearly</td>
            <td>
                <telerik:RadNumericTextBox ID="txtSpeak3WordAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="50px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>

        </tr>
        <tr>
            <td class="label">Speak 2 sentence clearly</td>
            <td>
                <telerik:RadNumericTextBox ID="txtSpeak2SentAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="50px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>

        </tr>
        <tr>
            <td class="label">School Class</td>
            <td >
                <telerik:RadTextBox ID="txtSchoolClass" runat="server" Width="100%" />
            </td>

        </tr>
        <tr>
            <td class="label">School Achievement</td>
            <td colspan="4">
                <telerik:RadTextBox ID="txtSchoolAchievement" runat="server" Width="100%" />
            </td>

        </tr>
        <tr>
            <td class="label">Current Growing Problem</td>
            <td colspan="4">
                <telerik:RadTextBox ID="txtGrowthNotes" runat="server" Width="100%" TextMode="MultiLine" Height="50px" />
            </td>

        </tr>
    </table>
</fieldset>
