<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="MealOrderDateInitializationDialog.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.MealOrderDateInitializationDialog" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtMealOrderDate" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="-- Select Meal Order Date then Save --">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFoodOrderDate" runat="server" Text="Meal Order Date"></asp:Label>
                </td>
                <td class="entry" style="width: 200px">
                    <telerik:RadDatePicker ID="txtMealOrderDate" runat="server" Width="100px" />
                </td>
                <td style="text-align: left">
                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/Toolbar/save16.png"
                        OnClick="btnSave_Click" ToolTip="Save" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
</asp:Content>
