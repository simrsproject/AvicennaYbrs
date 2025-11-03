<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="Census.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.Census" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../../../../JavaScript/DateFormat.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                __doPostBack("<%= grdModel1.UniqueID %>", args.get_item().get_value());
            }

            function OnOpenEditorDialog(id, smf, edit) {
                if (edit == 'True') {
                    var date = $find("<%= txtCensusDate.ClientID %>");
                    var unit = $find("<%= cboServiceUnitID.ClientID %>");
                    var cls = $find("<%= cboClassID.ClientID %>");

                    var oWnd = $find("<%= winProcess.ClientID %>");
                    oWnd.setUrl('CensusEditDialog.aspx?id=' + id + '&date=' + date.get_selectedDate().format("shortDate") + '&unit=' + unit.get_value() + '&cls=' + cls.get_value() + '&smf=' + smf);
                    oWnd.show();
                    //oWnd.maximize();
                    oWnd.add_pageLoad(onClientPageLoad);
                }
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument) {
                    if (oWnd.argument == 'rebind') {
                        __doPostBack("<%= grdModel1.UniqueID %>", "rebind");
                    }
                }
            }
        </script>

    </telerik:RadCodeBlock>
    
    <telerik:RadWindow runat="server" Animation="None" Width="900px" Height="550px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winProcess"
        OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadToolBar runat="server" ID="tbNavigation" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Resume Sensus Harian" Value="form1"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Sensus Harian Penderita Dirawat" Value="form2"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Census Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtCensusDate" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedDateChanged="txtCensusDate_SelectedDateChanged" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Service Unit
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Class
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboClassID" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboClassID_SelectedIndexChanged" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td class="entry">
                            <asp:Button ID="btnProcess" runat="server" Text="Process" OnClick="btnProcess_Click" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdModel1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
        OnNeedDataSource="grdModel1_NeedDataSource" OnItemDataBound="grdModel1_ItemDataBound"
        OnItemCommand="grdModel1_ItemCommand">
        <MasterTableView DataKeyNames="SmfID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SmfID" HeaderText="Smf ID"
                    UniqueName="SmfID" SortExpression="SmfID" HeaderStyle-HorizontalAlign="center"
                    ItemStyle-HorizontalAlign="center" Visible="false" />
                <telerik:GridBoundColumn DataField="SmfName" HeaderText="Smf" UniqueName="SmfName"
                    SortExpression="SmfName" />
                <telerik:GridTemplateColumn HeaderStyle-Width="85px" HeaderStyle-HorizontalAlign="center"
                    HeaderText="Pasien Hari Sebelumnya" ItemStyle-HorizontalAlign="center" UniqueName="TemplateColumn1"
                    DataField="Sebelumnya">
                    <ItemTemplate>
                        <telerik:RadTextBox runat="server" ID="txtSebelumnya" Width="100%" CssClass="RightAligned"
                            Text='<%# DataBinder.Eval(Container.DataItem, "Sebelumnya") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <telerik:RadTextBox runat="server" ID="txtSumSebelumnya" Width="100%" CssClass="RightAligned"
                            ReadOnly="true" Text='<%# DataBinder.Eval(Container.DataItem, "Sebelumnya") %>' />
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Sebelumnya" HeaderText="Pasien Hari Sebelumnya"
                    UniqueName="Sebelumnya" SortExpression="Sebelumnya" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="85px" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="Masuk" HeaderText="Pasien Masuk" UniqueName="Masuk"
                    SortExpression="Masuk" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="85px"
                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="Pindahan" HeaderText="Pasien Pindahan" UniqueName="Pindahan"
                    SortExpression="Pindahan" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="85px"
                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="Jumlah345" HeaderText="Jumlah (3+4+5)" UniqueName="Jumlah345"
                    SortExpression="Jumlah345" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="85px"
                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="Hidup" HeaderText="Pasien Keluar Hidup" UniqueName="Hidup"
                    SortExpression="Hidup" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="85px"
                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="Meninggal" HeaderText="Meninggal" UniqueName="Meninggal"
                    SortExpression="Meninggal" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="85px"
                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="Below48" HeaderText="< 48 Jam" UniqueName="Below48"
                    SortExpression="Below48" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="85px"
                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="Over48" HeaderText="> 48 Jam" UniqueName="Over48"
                    SortExpression="Over48" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="85px"
                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="Dipindahkan" HeaderText="Pasien Dipindahkan"
                    UniqueName="Dipindahkan" SortExpression="Dipindahkan" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="85px" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="Jumlah7811" HeaderText="Jumlah (7+8+11)" UniqueName="Jumlah7811"
                    SortExpression="Jumlah7811" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="85px"
                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="Dirawat" HeaderText="Pasien Dirawat" UniqueName="Dirawat"
                    SortExpression="Dirawat" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="85px"
                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
            </Columns>
            <NestedViewTemplate>
                <asp:Label runat="server" ID="lblCensusDate" Text='<%# Eval("CensusDate") %>' Visible="false" />
                <asp:Label runat="server" ID="lblServiceUnitID" Text='<%# Eval("ServiceUnitID") %>'
                    Visible="false" />
                <asp:Label runat="server" ID="lblClassID" Text='<%# Eval("ClassID") %>' Visible="false" />
                <asp:Label runat="server" ID="lblSmfID" Text='<%# Eval("SmfID") %>' Visible="false" />
                <table runat="server" id="InnerContainer" width="100%">
                    <tr>
                        <td width="20%" align="center" style="background-color: ButtonFace; height: 24px;">
                            <%# string.Format("<a href=\"#\" onclick=\"OnOpenEditorDialog('1', '{0}', '{1}'); return false;\">Pasien Masuk</a>", DataBinder.Eval(Container.DataItem, "SmfID"), IsUserEditAble)%>
                        </td>
                        <td width="20%" align="center" style="background-color: ButtonFace; height: 24px;">
                            <%# string.Format("<a href=\"#\" onclick=\"OnOpenEditorDialog('2', '{0}', '{1}'); return false;\">Pasien Pindahan</a>", DataBinder.Eval(Container.DataItem, "SmfID"), IsUserEditAble)%>
                        </td>
                        <td width="20%" align="center" style="background-color: ButtonFace; height: 24px;">
                            <%# string.Format("<a href=\"#\" onclick=\"OnOpenEditorDialog('3', '{0}', '{1}'); return false;\">Pasien Keluar Hidup</a>", DataBinder.Eval(Container.DataItem, "SmfID"), IsUserEditAble)%>
                        </td>
                        <td width="20%" align="center" style="background-color: ButtonFace; height: 24px;">
                            <%# string.Format("<a href=\"#\" onclick=\"OnOpenEditorDialog('4', '{0}', '{1}'); return false;\">Pasien Dipindahkan</a>", DataBinder.Eval(Container.DataItem, "SmfID"), IsUserEditAble)%>
                        </td>
                        <td width="20%" align="center" style="background-color: ButtonFace; height: 24px;">
                            <%# string.Format("<a href=\"#\" onclick=\"OnOpenEditorDialog('5', '{0}', '{1}'); return false;\">Pasien Meninggal</a>", DataBinder.Eval(Container.DataItem, "SmfID"), IsUserEditAble)%>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" valign="top">
                            <telerik:RadGrid ID="grdMasuk" runat="server" AutoGenerateColumns="false" DataSourceID="odsMasuk">
                                <MasterTableView DataKeyNames="RegistrationNo">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="Reg/RM/Bed">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "BedID") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn2" HeaderText="Nama/SMF/Kelas">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PatientName") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "ClassName") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "SmfName") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource runat="server" ID="odsMasuk" SelectMethod="SelectMasuk" TypeName="Temiang.Avicenna.Module.RADT.InPatient.Census">
                                <SelectParameters>
                                    <asp:ControlParameter Name="censusDate" ControlID="lblCensusDate" PropertyName="Text"
                                        Type="String" />
                                    <asp:ControlParameter Name="serviceUnitID" ControlID="lblServiceUnitID" PropertyName="Text"
                                        Type="String" />
                                    <asp:ControlParameter Name="classID" ControlID="lblClassID" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter Name="smfID" ControlID="lblSmfID" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td width="20%" valign="top">
                            <telerik:RadGrid ID="grdPindahan" runat="server" AutoGenerateColumns="false" DataSourceID="odsPindahan">
                                <MasterTableView DataKeyNames="RegistrationNo">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="Reg/RM/Bed">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "ToBedID")%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn2" HeaderText="Nama/SMF/Kelas">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PatientName") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "FromServiceUnitName")%>
                                                →
                                                <%# DataBinder.Eval(Container.DataItem, "ToServiceUnitName")%><br />
                                                <%# DataBinder.Eval(Container.DataItem, "FromClassName") %>
                                                →
                                                <%# DataBinder.Eval(Container.DataItem, "ToClassName") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "FromBedID") %>
                                                →
                                                <%# DataBinder.Eval(Container.DataItem, "ToBedID") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "FromSmfName") %>
                                                →
                                                <%# DataBinder.Eval(Container.DataItem, "ToSmfName") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource runat="server" ID="odsPindahan" SelectMethod="SelectPindahan"
                                TypeName="Temiang.Avicenna.Module.RADT.InPatient.Census">
                                <SelectParameters>
                                    <asp:ControlParameter Name="censusDate" ControlID="lblCensusDate" PropertyName="Text"
                                        Type="String" />
                                    <asp:ControlParameter Name="serviceUnitID" ControlID="lblServiceUnitID" PropertyName="Text"
                                        Type="String" />
                                    <asp:ControlParameter Name="classID" ControlID="lblClassID" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter Name="smfID" ControlID="lblSmfID" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td width="20%" valign="top">
                            <telerik:RadGrid ID="grdHidup" runat="server" AutoGenerateColumns="false" DataSourceID="odsHidup">
                                <MasterTableView DataKeyNames="RegistrationNo">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="Reg/RM/Bed">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "BedID") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn2" HeaderText="Nama/SMF/Kelas">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PatientName") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "ClassName") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "SmfName") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource runat="server" ID="odsHidup" SelectMethod="SelectHidup" TypeName="Temiang.Avicenna.Module.RADT.InPatient.Census">
                                <SelectParameters>
                                    <asp:ControlParameter Name="censusDate" ControlID="lblCensusDate" PropertyName="Text"
                                        Type="String" />
                                    <asp:ControlParameter Name="serviceUnitID" ControlID="lblServiceUnitID" PropertyName="Text"
                                        Type="String" />
                                    <asp:ControlParameter Name="classID" ControlID="lblClassID" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter Name="smfID" ControlID="lblSmfID" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td width="20%" valign="top">
                            <telerik:RadGrid ID="grdDipindahkan" runat="server" AutoGenerateColumns="false" DataSourceID="odsDipindahkan">
                                <MasterTableView DataKeyNames="RegistrationNo">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="Reg/RM/Bed">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "BedID") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn2" HeaderText="Nama/SMF/Kelas">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PatientName") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "FromServiceUnitName")%>
                                                →
                                                <%# DataBinder.Eval(Container.DataItem, "ToServiceUnitName")%><br />
                                                <%# DataBinder.Eval(Container.DataItem, "FromClassName")%>
                                                →
                                                <%# DataBinder.Eval(Container.DataItem, "ToClassName") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "FromBedID") %>
                                                →
                                                <%# DataBinder.Eval(Container.DataItem, "ToBedID") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "FromSmfName") %>
                                                →
                                                <%# DataBinder.Eval(Container.DataItem, "ToSmfName") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource runat="server" ID="odsDipindahkan" SelectMethod="SelectDipindahkan"
                                TypeName="Temiang.Avicenna.Module.RADT.InPatient.Census">
                                <SelectParameters>
                                    <asp:ControlParameter Name="censusDate" ControlID="lblCensusDate" PropertyName="Text"
                                        Type="String" />
                                    <asp:ControlParameter Name="serviceUnitID" ControlID="lblServiceUnitID" PropertyName="Text"
                                        Type="String" />
                                    <asp:ControlParameter Name="classID" ControlID="lblClassID" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter Name="smfID" ControlID="lblSmfID" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td width="20%" valign="top">
                            <telerik:RadGrid ID="grdMeninggal" runat="server" AutoGenerateColumns="false" DataSourceID="odsMeninggal">
                                <MasterTableView DataKeyNames="RegistrationNo">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="Reg/RM/Bed">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "BedID") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn2" HeaderText="Nama/SMF/Kelas">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PatientName") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "ClassName") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "SmfName") %><br />
                                                <%# DataBinder.Eval(Container.DataItem, "Condition") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource runat="server" ID="odsMeninggal" SelectMethod="SelectMeninggal"
                                TypeName="Temiang.Avicenna.Module.RADT.InPatient.Census">
                                <SelectParameters>
                                    <asp:ControlParameter Name="censusDate" ControlID="lblCensusDate" PropertyName="Text"
                                        Type="String" />
                                    <asp:ControlParameter Name="serviceUnitID" ControlID="lblServiceUnitID" PropertyName="Text"
                                        Type="String" />
                                    <asp:ControlParameter Name="classID" ControlID="lblClassID" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter Name="smfID" ControlID="lblSmfID" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </NestedViewTemplate>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" HorizontalAlign="NotSet" />
</asp:Content>
