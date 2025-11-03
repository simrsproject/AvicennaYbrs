<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ParamedicFeeByServiceSettingSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFee.ParamedicFeeByServiceSettingSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lbl1" runat="server" Text="Registration Type" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:radcombobox id="cboSRRegistrationType" runat="server" width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Service Unit" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:radcombobox id="cboServiceUnit" runat="server" width="300px"
                    onitemdatabound="cboServiceUnit_ItemDataBound"
                    onitemsrequested="cboServiceUnit_ItemsRequested">
                </telerik:radcombobox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Item" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:radcombobox id="cboItem" runat="server" width="304px"
                    enableloadondemand="True" highlighttemplateditems="True" markfirstmatch="False"
                    onitemdatabound="cboItem_ItemDataBound"
                    onitemsrequested="cboItem_ItemsRequested">
                </telerik:radcombobox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Class" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:radcombobox id="cboClass" runat="server" width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label4" runat="server" Text="Paramedic Fee Case Type" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:radcombobox id="cboSRParamedicFeeCaseType" runat="server" width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label5" runat="server" Text="Paramedic Fee Is Team" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:radcombobox id="cboSRParamedicFeeIsTeam" runat="server" width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label6" runat="server" Text="Paramedic FeeTeam Status" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:radcombobox id="cboSRParamedicFeeTeamStatus" runat="server" width="300px" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
