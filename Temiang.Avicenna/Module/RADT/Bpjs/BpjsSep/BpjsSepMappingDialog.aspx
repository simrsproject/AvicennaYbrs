<%@  Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="BpjsSepMappingDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.BpjsSepMappingDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrasi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDetailRegistrasi" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td witdh="50%" valign="top">
                <telerik:RadTextBox runat="server" ID="txtDetailSEP" Width="99%" ReadOnly="true"
                    Height="400px" TextMode="MultiLine" />
            </td>
            <td witdh="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            No Registrasi
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtNoRegistrasi" Width="300px" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnFilterRegistrasi" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterRegistrasi_Click" ToolTip="Cari Registrasi" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td witdh="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="No Registrasi required."
                                ValidationGroup="entry" ControlToValidate="txtNoRegistrasi" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <telerik:RadTextBox runat="server" ID="txtDetailRegistrasi" Width="99%" ReadOnly="true"
                                Height="300px" TextMode="MultiLine" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
