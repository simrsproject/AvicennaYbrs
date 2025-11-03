<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="BpjsSepList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.BpjsSepList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../../../../JavaScript/jquery.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinMapping(sepNo, regNo) {

            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.mode != null) __doPostBack("<%= grdList.UniqueID %>", oWnd.argument.mode);
            }

            function gotoAddUrl(sepno) {
                var oWnd = $find("<%= winRujukan.ClientID %>");
                oWnd.setUrl('VClaim/UpdateTanggalPulangDialog.aspx?sepno=' + sepno);
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Timer1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMessage" />
                    <telerik:AjaxUpdatedControl ControlID="imgOk" />
                    <telerik:AjaxUpdatedControl ControlID="imgFailed" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        Width="750px" Height="500px" VisibleStatusbar="False" Modal="true" ID="winRujukan"
        OnClientClose="onClientClose" />
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
        AllowSorting="True" GridLines="None" OnDeleteCommand="grdList_DeleteCommand">
        <MasterTableView DataKeyNames="SepID, NoSEP" ClientDataKeyNames="SepID, NoSEP">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn" Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="160px" HeaderText="No SEP / No Registrasi">
                    <ItemTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <%# string.Format("<a href=\"#\" onclick=\"openWinMapping('{0}', '{1}'); return false;\"><b>{0}</b></a>", DataBinder.Eval(Container.DataItem, "NoSEP"), DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalSEP" HeaderText="Tgl SEP"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TanggalSEP" SortExpression="TanggalSEP"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="NoRujukan" HeaderText="No Rujukan"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="NoRujukan" SortExpression="NoRujukan"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalRujukan" HeaderText="Tgl Rujukan"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TanggalRujukan" SortExpression="TanggalRujukan"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="NomorKartu" HeaderText="No Kartu"
                    UniqueName="NomorKartu" SortExpression="NomorKartu" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="NamaPasienJK" HeaderText="Nama Pasien (JK)" UniqueName="NamaPasienJK"
                    SortExpression="NamaPasienJK" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalLahir" HeaderText="Tgl Lahir"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TanggalLahir" SortExpression="TanggalLahir"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="TypeOfService" HeaderText="Jenis Pelayanan" UniqueName="TypeOfService"
                    SortExpression="TypeOfService" />
                <telerik:GridBoundColumn DataField="BridgingName" HeaderText="Poli Tujuan" UniqueName="BridgingName"
                    SortExpression="BridgingName" />
                <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnosa Awal" UniqueName="DiagnoseName"
                    SortExpression="DiagnoseName" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsLakaLantas" HeaderText="Laka Lantas"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="IsLakaLantas" SortExpression="IsLakaLantas"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsRegistration" HeaderText="Registrasi"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="IsRegistration" SortExpression="IsLakaLantas"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="User Update" UniqueName="LastUpdateByUserID"
                    SortExpression="LastUpdateByUserID" />
                <telerik:GridTemplateColumn UniqueName="Update" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}');\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Update Tanggal Pulang\" /></a>", DataBinder.Eval(Container.DataItem, "NoSEP"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?" >
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <div class="footer" style="display: none">
        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="60000" Enabled="False" />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr align="center">
                <td align="center">
                    <table>
                        <tr>
                            <td width="16px">
                                <asp:Image ID="imgOk" runat="server" ImageUrl="~/Images/Toolbar/post_green_16.png"
                                    Visible="false" />
                                <asp:Image ID="imgFailed" runat="server" ImageUrl="~/Images/Toolbar/blacklist.png"
                                    Visible="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblMessage" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
