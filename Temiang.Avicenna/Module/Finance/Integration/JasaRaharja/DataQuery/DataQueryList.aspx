<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="DataQueryList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Integration.JasaRaharja.DataQueryList" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <iframe id="my_iframe" style="display: none;"></iframe>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function openWinRegistrationInfo(id, regno, type) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                if (type == 'info')
                    oWnd.setUrl('DataQueryDialog.aspx?id=' + id);
                else
                    oWnd.setUrl('UploadDocumentDialog.aspx?id=' + id + '&regno=' + regno);
                oWnd.show();
            }

            function downloadDocument(url) {
                //alert(url);
                document.getElementById('my_iframe').src = url;
            }

            function onWinUploadClientClose(oWnd) {
                var arg = oWnd.argument;
                if (arg) {
                    if (oWnd.argument.isSuccess == '') return;
                    if (oWnd.argument.isSuccess == 'true')
                        alert('Upload file success.');
                    else if (oWnd.argument.isSuccess == 'false')
                        alert('Upload file failed, please try again.');
                    oWnd.argument.isSuccess = '';

                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
                }
                oWnd = null;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" OnClientClose="onWinUploadClientClose" />
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                Registration No
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" AllowPaging="true" AutoGenerateColumns="false"
        OnDetailTableDataBind="grdList_DetailTableDataBind" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ID_REGISTER" ClientDataKeyNames="ID_REGISTER">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn0">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openWinRegistrationInfo('{0}', '{1}', 'upload'); return false;\"><img src=\"../../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"Upload\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ID_REGISTER"), DataBinder.Eval(Container.DataItem, "KODE_KEJADIAN"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ID_REGISTER" HeaderText="ID REGISTER" UniqueName="ID_REGISTER"
                    SortExpression="ID_REGISTER" />
                <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="NAMA KORBAN">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openWinRegistrationInfo('{0}', '', 'info'); return false;\">{1}</a>",
                                DataBinder.Eval(Container.DataItem, "KODE_KEJADIAN"), DataBinder.Eval(Container.DataItem, "NAMA_KORBAN"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SIFAT_CEDERA" HeaderText="SIFAT CEDERA" UniqueName="SIFAT_CEDERA"
                    SortExpression="SIFAT_CEDERA" />
                <telerik:GridBoundColumn DataField="JENIS_TINDAKAN" HeaderText="JENIS TINDAKAN" UniqueName="JENIS_TINDAKAN"
                    SortExpression="JENIS_TINDAKAN" />
                <telerik:GridBoundColumn DataField="DOKTER_BERWENANG" HeaderText="DOKTER BERWENANG"
                    UniqueName="DOKTER_BERWENANG" SortExpression="DOKTER_BERWENANG" />
                <telerik:GridNumericColumn DataField="BIAYA" HeaderText="JML BIAYA" UniqueName="BIAYA"
                    SortExpression="BIAYA" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                    DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn DataField="JUMLAH_DIBAYARKAN" HeaderText="JML KLAIM" UniqueName="JUMLAH_DIBAYARKAN"
                    SortExpression="JUMLAH_DIBAYARKAN" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
                    DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn DataField="TGL_PROSES" HeaderText="TGL PROSES" UniqueName="TGL_PROSES"
                    SortExpression="TGL_PROSES" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="STATUS_JAMINAN" HeaderText="STATUS JAMINAN" UniqueName="STATUS_JAMINAN"
                    SortExpression="STATUS_JAMINAN" />
                <telerik:GridCheckBoxColumn DataField="STATUS_KLAIM" HeaderText="STATUS KLAIM" UniqueName="STATUS_KLAIM"
                    SortExpression="STATUS_KLAIM" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn UniqueName="TemplateColumn2" HeaderText="NO SURAT JAMINAN">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"{0}\">{1}</a>",
                                DataBinder.Eval(Container.DataItem, "PATH_SURAT_JAMINAN"), DataBinder.Eval(Container.DataItem, "NO_SURAT_JAMINAN"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="NAMA_FILE" AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="DESKRIPSI" HeaderText="DESKRIPSI" UniqueName="DESKRIPSI"
                            SortExpression="DESKRIPSI" />
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn3" HeaderText="DOKUMEN KORBAN">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"{0}\">{1}</a>",
                                    DataBinder.Eval(Container.DataItem, "FILE_PATH"), DataBinder.Eval(Container.DataItem, "NAMA_FILE"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
