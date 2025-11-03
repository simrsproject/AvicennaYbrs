<%@ Page Title="Fee Detail" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" 
CodeBehind="CoaEditBkuDialog.aspx.cs" 
Inherits="Temiang.Avicenna.Module.Finance.CoaEditBkuDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script language="javascript" type="text/javascript">
        </script>
    </telerik:RadCodeBlock>

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 100%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Chart Of Account
                        </td>
                        <td class="entry">
                            <asp:Label ID="lblCoa" runat="server"></asp:Label>
                        </td>
                        <td style="width: 20px;">

                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lbl1" runat="server" Text="Bku Account"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboBkuAccount" runat="server" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboBkuAccount_ItemDataBound"
                                OnItemsRequested="cboBkuAccount_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items                                  
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br /><br /><br />
</asp:Content>