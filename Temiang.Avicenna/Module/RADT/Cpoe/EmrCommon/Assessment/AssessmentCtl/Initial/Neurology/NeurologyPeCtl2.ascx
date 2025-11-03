<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NeurologyPeCtl2.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.NeurologyPeCtl2" %>
<%@ Register TagPrefix="uc1" TagName="GcsCtl" Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<uc1:GcsCtl runat="server" ID="gcsCtl" />
<table style="width: 100%;">
</table>
<fieldset>
    <legend>Stimulus Sign Meningeal</legend>
    <table style="width: 100%;">
        <tr>
            <td class="label">Nuchal Rigidity
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optNuchalRigidity" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
        <tr>
            <td class="label">Kernig
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optKernig" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
        <tr>
            <td class="label">Lasgque
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optLasgque" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>Funduscopy</legend>
    <table style="width: 100%;">
        <tr>
            <td class="label">Papiledema
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optPapiledema" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
    </table>
</fieldset>

<table style="width: 100%;">
    <tr>
        <td class="label">Nervus Cranialis</td>
        <td>
            <telerik:RadDropDownList ID="ddlCranialis" runat="server" Width="250px"  />
        </td>
    </tr>
</table>
<fieldset>
    <legend>Status Motorik</legend>
    <table style="width: 50%;">
        <tr>
            <td class="label">Extermintas Superior
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optExtermintasSuperior" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
        <tr>
            <td class="label">Extermintas Interior
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optExtermintasInterior" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
    </table>
</fieldset>

<table style="width: 100%;">
    <tr>
        <td class="label">Refleks Fisiologis</td>
        <td>
            <telerik:RadTextBox ID="txtRefleksFisiologis" runat="server" Width="100%" />
        </td>
    </tr>
    <tr>
        <td class="label">Refleks Patologis</td>
        <td>
            <telerik:RadTextBox ID="txtRefleksPatologis" runat="server" Width="100%" />
        </td>
    </tr>
    <tr>
        <td class="label">Status Otonom
        </td>
        <td>
            <telerik:RadTextBox ID="txtStatusOtonom" runat="server" Width="100%" />
        </td>
    </tr>

</table>
<table style="width: 100%;">
    <tr>
        <td class="label">Physical Exam
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>
    <tr>
        <td class="label">Neurologis
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNeurologis" runat="server" Width="100%" TextMode="MultiLine" Height="100px" Resize="Vertical" />
        </td>
    </tr>

</table>
