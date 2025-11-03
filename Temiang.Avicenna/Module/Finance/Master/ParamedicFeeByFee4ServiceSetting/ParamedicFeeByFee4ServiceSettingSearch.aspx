<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeeByFee4ServiceSettingSearch.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeByFee4ServiceSettingSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboFilterItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="ID" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtID" runat="server" Width="50px" NumberFormat-DecimalDigits="0" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label12" runat="server" Text="Paramedic Status" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboParamedicStatus" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label7" runat="server" Text="Paramedic" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboParamedic" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr style="display:none;">
            <td class="label">
                <asp:Label ID="Label4" runat="server" Text="Specialty" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSpecialty" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lbl1" runat="server" Text="Registration Type" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRRegistrationType" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label5" runat="server" Text="Tariff Type" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboTariffType" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label10" runat="server" Text="Class" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboClass" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label11" runat="server" Text="Guarantor Type" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboGuarantorType" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label8" runat="server" Text="Guarantor" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboGuarantor" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Service Unit" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboServiceUnit" runat="server" Width="300px" OnItemDataBound="cboServiceUnit_ItemDataBound"
                    OnItemsRequested="cboServiceUnit_ItemsRequested">
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label9" runat="server" Text="Item Group" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboItemGroup" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Item" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterItem" runat="server" Width="100px" AutoPostBack="true"  
                    OnSelectedIndexChanged="cboFilterItem_SelectedIndexChanged">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboItem" runat="server" Width="300px" EnableLoadOnDemand="True"
                    HighlightTemplatedItems="True" MarkFirstMatch="False" OnItemDataBound="cboItem_ItemDataBound"
                    OnItemsRequested="cboItem_ItemsRequested">
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label6" runat="server" Text="Procedure Name" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRProcedure" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
