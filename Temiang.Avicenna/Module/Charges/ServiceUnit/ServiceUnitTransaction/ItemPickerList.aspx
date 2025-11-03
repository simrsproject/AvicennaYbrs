<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ItemPickerList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ItemPickerList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function LoadItemByGroup(itemGroupID) {
            __doPostBack("itemGroupID", itemGroupID);
        }
    </script>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server" />
    <table width="100%" id="table2" runat="server" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top" width="20%">
                <table>
                    <tr runat="server" id="trCitoOption">
                        <td>Cito Option</td>
                        <td></td>
                        <td>
                            <telerik:RadComboBox ID="cboSRCitoPercentage" runat="server" Width="250px"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trPhysician">
                        <td>Physician</td>
                        <td></td>
                        <td>
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="250px"></telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" id="table0" runat="server" cellpadding="1" cellspacing="5">
        <tr>
            <td></td>
        </tr>
    </table>
    <table width="100%" id="table1" runat="server" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top" width="20%"></td>
            <td valign="top" width="20%"></td>
            <td valign="top" width="20%"></td>
            <td valign="top" width="20%"></td>
            <td valign="top" width="20%"></td>
        </tr>
    </table>
    <br />
    <br />
    <br />
</asp:Content>
