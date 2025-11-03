<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="MedicalRecordFileAnalysisDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.MedicalRecordFileAnalysisDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGender" runat="server" Text="Gender / Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGender" runat="server" Width="80px" ReadOnly="True" />
                            <telerik:RadTextBox ID="txtAge" runat="server" Width="80px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="100%" ReadOnly="True"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="lblanalysis" runat="server" Text="Analisys Information"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDepartmentID" runat="server" Text="Department"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDepartmentID" runat="server" Width="100px" ReadOnly="True" Visible="false" />
                            <telerik:RadTextBox ID="txtDepartmentName" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRFilesAnalysis" runat="server" Text="Files Analysis"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRFilesAnalysis" runat="server" Width="100%" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRFilesAnalysis_ItemDataBound"
                                OnItemsRequested="cboSRFilesAnalysis_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboSRFilesAnalysis_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "FilesAnalysisName")%>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRFilesAnalysis" runat="server" ErrorMessage="Files Analysis required."
                                ValidationGroup="entry" ControlToValidate="cboSRFilesAnalysis" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="100%" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ParamedicName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRCompleteStatusRM" runat="server" Text="Complete Status RM"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRCompleteStatusRM" runat="server" Width="100%" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFilesReceiveDate" runat="server" Text="Files Receive Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtFilesReceiveDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFilesReceiveDate" runat="server" ErrorMessage="Files Receive Date required."
                                ValidationGroup="entry" ControlToValidate="txtFilesReceiveDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFilesAcceptanceDate" runat="server" Text="Files Acceptance Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtFilesAcceptanceDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdAnalysisDocumentItem" runat="server" AutoGenerateColumns="false"
        AllowMultiRowSelection="true" OnNeedDataSource="grdAnalysisDocumentItem_NeedDataSource">
        <MasterTableView DataKeyNames="DocumentFilesID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="DocumentFilesID" HeaderText="ID"
                    UniqueName="DocumentFilesID" SortExpression="DocumentFilesID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="DocumentName" HeaderText="Document Name"
                    UniqueName="DocumentName" SortExpression="DocumentName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="DocumentNumber" HeaderText="Document Number"
                    UniqueName="DocumentNumber" SortExpression="DocumentNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn UniqueName="IsQuantity" HeaderText="Quantity">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsQuantity") %>'
                            ID="chkIsQuantity" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsQuality" UniqueName="IsQuality" HeaderText="Quality"
                    HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsQuality" Visible='<%# DataBinder.Eval(Container.DataItem, "IsQualityCek") %>'
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsQuality") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsSign" UniqueName="IsSign" HeaderText="Sign"
                    HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsSign" Visible='<%# DataBinder.Eval(Container.DataItem, "IsSignCek") %>'
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsSign") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsLegible" UniqueName="IsLegible" HeaderText="Legible"
                    HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsLegible" Visible='<%# DataBinder.Eval(Container.DataItem, "IsLegibleCek") %>'
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsLegible") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True"></Selecting>
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
