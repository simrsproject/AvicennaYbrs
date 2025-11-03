<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="RujukBalikDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.RujukBalikDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../../../../JavaScript/jquery.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function OnClientItemsRequestingHandlerDpjp(sender, eventArgs) {
                if (sender.get_text().length < 3) eventArgs.set_cancel(true);
                else {
                    var context = eventArgs.get_context();
                    context["filter"] = eventArgs.get_text() + "||";
                }
            }

            function OnClientItemsRequestingHandlerObat(sender, eventArgs) {
                if (sender.get_text().length < 3) eventArgs.set_cancel(true);
                else {
                    var context = eventArgs.get_context();
                    context["filter"] = eventArgs.get_text();
                }
            }

            function openWinSearchSep() {
                var jp = $find("<%= txtNoSep.ClientID %>");
                if (jp.get_value() == '') {
                    alert('No SEP pasien belum diisi.');
                    return;
                }

                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl("../../Registration/BPJSRegistrationInfo.aspx?type=ruj&sep=" + jp.get_value());
                oWnd.set_title('Avicenna - BPJS Info');
                oWnd.set_width($(window).width() - 100);
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.sep != null) {
                    if (oWnd.argument.sep != '') {
                        var jp = $find("<%= txtNoSep.ClientID %>");
                        jp.set_value = oWnd.argument.sep;
                        __doPostBack("<%= txtNoSep.UniqueID %>", "rebind");
                    }
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        Width="750px" Height="420px" VisibleStatusbar="False" Modal="true" ID="winRujukan"
        OnClientClose="onClientClose" />
    <table width="100%">
        <tr>
            <td width="50%" valign="top">
                <table>
                    <tr>
                        <td class="label">No SRB</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNoSRB" runat="server" ReadOnly="true" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">No SEP</td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtNoSep" runat="server" Width="300px" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnCariPasien" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClientClick="openWinSearchSep(); return false;" ToolTip="Cari Sep" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvNoSep" runat="server" ErrorMessage="No SEP required."
                                ValidationGroup="entry" ControlToValidate="txtNoSep" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">No Peserta</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNoPeserta" runat="server" ReadOnly="true" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Nama Peserta</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNamaPeserta" runat="server" ReadOnly="true" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Alamat</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAlamat" runat="server" TextMode="MultiLine" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">E-Mail</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">DPJP</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDpjp" runat="server" Width="304px" AllowCustomText="true"
                                OnItemDataBound="cboDpjp_ItemDataBound" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandlerDpjp">
                                <WebServiceSettings Path="../../../../WebService/Sep.asmx" Method="GetDPJP" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvDPJP" runat="server" ErrorMessage="DPJP required."
                                ValidationGroup="entry" ControlToValidate="cboDpjp" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Program PRB</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboProgramPrb" runat="server" Width="304px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvProgramPRB" runat="server" ErrorMessage="Program PRB required."
                                ValidationGroup="entry" ControlToValidate="cboProgramPrb" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Keterangan</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtKeterangan" runat="server" TextMode="MultiLine" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Saran</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSaran" runat="server" TextMode="MultiLine" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Nama Obat</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboKodeObat" runat="server" Width="304px" AllowCustomText="true"
                                OnItemDataBound="cboKodeObat_ItemDataBound" EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequestingHandlerObat">
                                <WebServiceSettings Path="../../../../WebService/Sep.asmx" Method="GetObatPRB" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Jml Obat</td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtJmlObat" runat="server" Width="100px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Signa 1</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSigna1" runat="server" TextMode="MultiLine" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Signa 2</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSigna2" runat="server" TextMode="MultiLine" Width="300px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:Button ID="btnAddObat" runat="server" Text="Tambah Obat" OnClick="btnAddObat_Click" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td colspan="4">
                            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnDeleteCommand="grdList_DeleteCommand"
                                AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
                                AllowSorting="True" GridLines="None">
                                <MasterTableView DataKeyNames="KdObat" ClientDataKeyNames="KdObat">
                                    <Columns>
                                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="KdObat" HeaderText="Kode Obat"
                                            HeaderStyle-HorizontalAlign="Center" UniqueName="KdObat" SortExpression="KdObat"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn DataField="NmObat" HeaderText="Nama Obat" UniqueName="NmObat"
                                            SortExpression="NmObat" />
                                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="JmlObat" HeaderText="Jml Obat"
                                            HeaderStyle-HorizontalAlign="Center" UniqueName="JmlObat" SortExpression="JmlObat"
                                            ItemStyle-HorizontalAlign="Right" />
                                        <telerik:GridBoundColumn DataField="Signa1" HeaderText="Signa 1" UniqueName="Signa1"
                                            SortExpression="Signa1" />
                                        <telerik:GridBoundColumn DataField="Signa2" HeaderText="Signa 2" UniqueName="Signa2"
                                            SortExpression="Signa2" />
                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
