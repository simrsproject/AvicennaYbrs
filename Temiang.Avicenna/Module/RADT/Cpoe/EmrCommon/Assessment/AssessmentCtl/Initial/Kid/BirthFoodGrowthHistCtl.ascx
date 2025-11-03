<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BirthFoodGrowthHistCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.BirthFoodGrowthHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<fieldset style="width: 49%;">
    <legend>PERINATAL HISTORY</legend>
    <table width="100%">
        <tr>
            <td class="label">Birth Method</td>
            <td style="width: 240px">
                <telerik:RadComboBox runat="server" ID="cboSRBirthMethodPhr" Width="100%">
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtMethodOther" Width="100%">
                </telerik:RadTextBox></td>
        </tr>
        <tr runat="server" visible ="false">
            <td class="label">Birth Method</td>
            <td style="width: 120px;">
                <asp:RadioButtonList ID="optBirthMethod" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="Spontaneous" Value="SN" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="SC on indications" Value="SC"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td valign="bottom">
                <telerik:RadTextBox ID="txtBirthMethodScIndication" runat="server" Width="100%" />
            </td>
        </tr>
    </table>
</fieldset>

<fieldset style="width: 49%;">
    <legend>GROWING HISTORY</legend>
    <table width="100%">

        <tr>
            <td class="label">Child number</td>
            <td colspan="2">
                <telerik:RadNumericTextBox NumberFormat-DecimalDigits="0" ID="txtChildNumber" runat="server" Width="80px" />
                &nbsp;from&nbsp;<telerik:RadNumericTextBox NumberFormat-DecimalDigits="0" ID="txtChildNumberFrom" runat="server" Width="80px" />
            </td>

        </tr>
        <tr>
            <td class="label">Birth head circumference</td>
            <td>
                <telerik:RadNumericTextBox ID="txtHeadCircum" NumberFormat-DecimalDigits="0" runat="server" Width="80px" />
            </td>
            <td>cm</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Birth weight</td>
            <td>
                <telerik:RadNumericTextBox NumberFormat-DecimalDigits="2" ID="txtWeight" runat="server" Width="80px" />
            </td>
            <td>gr</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Birth length</td>
            <td>
                <telerik:RadNumericTextBox NumberFormat-DecimalDigits="0" ID="txtLength" runat="server" Width="80px" />
            </td>
            <td>cm</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">ASI to age</td>
            <td style="width: 30px">
                <telerik:RadNumericTextBox ID="txtAsiToMonthAge" NumberFormat-DecimalDigits="0" runat="server" Width="80px" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Formula milk start age</td>
            <td style="width: 30px">
                <telerik:RadNumericTextBox ID="txtFormulaMilkStartAge" NumberFormat-DecimalDigits="0" runat="server" Width="80px" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Additional food start age</td>
            <td style="width: 30px">
                <telerik:RadNumericTextBox ID="txtAddFoodStartAge" NumberFormat-DecimalDigits="0" runat="server" Width="80px" />
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
                <telerik:RadNumericTextBox ID="txtRaiseHead" runat="server" NumberFormat-DecimalDigits="0" Width="80px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">On his stomach</td>
            <td>
                <telerik:RadNumericTextBox ID="txtProneAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="80px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Turn Over</td>
            <td>
                <telerik:RadNumericTextBox ID="txtTurnOverAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="80px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Sitdown</td>
            <td>
                <telerik:RadNumericTextBox ID="txtSitAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="80px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Crawl</td>
            <td>
                <telerik:RadNumericTextBox ID="txtCrawlAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="80px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>

        </tr>
        <tr>
            <td class="label">Stand up</td>
            <td>
                <telerik:RadNumericTextBox ID="txtStandUpAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="80px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>

        </tr>
        <tr>
            <td class="label">Walk</td>
            <td>
                <telerik:RadNumericTextBox ID="txtWalkAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="80px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Grabbing objects</td>
            <td>
                <telerik:RadNumericTextBox ID="txtGrabbing" runat="server" NumberFormat-DecimalDigits="0" Width="80px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Holding objects</td>
            <td>
                <telerik:RadNumericTextBox ID="txtHolding" runat="server" NumberFormat-DecimalDigits="0" Width="80px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>
        </tr>

        <tr>
            <td class="label">Speak 3 - 6 word clearly</td>
            <td>
                <telerik:RadNumericTextBox ID="txtSpeak3WordAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="80px" NumberFormat="###" />
            </td>
            <td>Month</td>
            <td></td>

        </tr>
        <tr>
            <td class="label">Speak 2 sentence clearly</td>
            <td>
                <telerik:RadNumericTextBox ID="txtSpeak2SentAtMonthAge" runat="server" NumberFormat-DecimalDigits="0" Width="80px" NumberFormat="###" />
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
