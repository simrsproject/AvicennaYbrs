<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ClinicalPathwayDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ClinicalPathwayDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Pathway ID
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtPathwayID" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPathwayID" runat="server" ErrorMessage="Pathway ID required."
                                ControlToValidate="txtPathwayID" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Pathway Name
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtPathwayName" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPathwayName" runat="server" ErrorMessage="Pathway Name required."
                                ControlToValidate="txtPathwayName" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Starting Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker runat="server" ID="txtStartingDate" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvStartingDate" runat="server" ErrorMessage="Starting Date required."
                                ControlToValidate="txtStartingDate" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label">A L O S
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox runat="server" ID="txtALOS" Width="100px" MinValue="0"
                                MaxValue="7" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvALOS" runat="server" ErrorMessage="A L O S required."
                                ControlToValidate="txtALOS" SetFocusOnError="True" ValidationGroup="entry" Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">Coverage Value (Total)
                        </td>
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td class="label">Class I (Rp.)
                                    </td>
                                    <td class="label">Class II (Rp.)
                                    </td>
                                    <td class="label">Class III (Rp.)
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtClass1" Width="100px" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtClass2" Width="100px" />
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtClass3" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Notes
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsActive" Text="Active" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Load From Data</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboLoadFromData" Width="304px" AllowCustomText="true" AutoPostBack="true"
                                EnableLoadOnDemand="true" OnItemsRequested="cboLoadFromData_ItemsRequested" OnItemDataBound="cboLoadFromData_ItemDataBound" />
                        </td>
                        <td width="40px">
                            <asp:ImageButton ID="imgLoad" runat="server" ImageUrl="~/Images/Toolbar/refresh16.png" OnClick="imgLoad_Click" OnClientClick="if(confirm('Are you sure?')==false)return false;" />
                            <asp:ImageButton ID="imgClear" runat="server" ImageUrl="~/Images/Toolbar/cancel16.png" OnClick="imgClear_Click" OnClientClick="if(confirm('Are you sure?')==false)return false;" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="MultiPage1" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Diagnose" PageViewID="pgRiwayat" />
            <telerik:RadTab runat="server" Text="Activities" PageViewID="pgDetail" Selected="True" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="MultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgDetail" runat="server" Selected="true">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                OnUpdateCommand="grdList_UpdateCommand" OnDeleteCommand="grdList_DeleteCommand"
                ShowFooter="true" OnInsertCommand="grdList_InsertCommand" AutoGenerateColumns="false"
                OnItemDataBound="grdList_ItemDataBound">
                <MasterTableView DataKeyNames="PathwayID, PathwayItemSeqNo" CommandItemDisplay="None">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="AssesmentHeaderName" HeaderText="Header Name "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="AssesmentHeaderName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="AssesmentGroupName" HeaderText="Group Name "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="AssesmentGroupName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="AssesmentGroupName" HeaderText="Group Name" UniqueName="AssesmentGroupName"
                            SortExpression="AssesmentGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="AssesmentName" HeaderText="Activity Name" UniqueName="AssesmentName"
                            SortExpression="AssesmentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CoverageValue1" HeaderText="Class I (Rp.)"
                            UniqueName="CoverageValue1" SortExpression="CoverageValue1" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                            DataFormatString="{0:n2}" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CoverageValue2" HeaderText="Class II (Rp.)"
                            UniqueName="CoverageValue2" SortExpression="CoverageValue2" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                            DataFormatString="{0:n2}" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CoverageValue3" HeaderText="Class III (Rp.)"
                            UniqueName="CoverageValue3" SortExpression="CoverageValue3" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                            DataFormatString="{0:n2}" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="col_1" HeaderText="Day 1"
                            UniqueName="col_1" SortExpression="col_1" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="col_2" HeaderText="Day 2"
                            UniqueName="col_2" SortExpression="col_2" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="col_3" HeaderText="Day 3"
                            UniqueName="col_3" SortExpression="col_3" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="col_4" HeaderText="Day 4"
                            UniqueName="col_4" SortExpression="col_4" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="col_5" HeaderText="Day 5"
                            UniqueName="col_5" SortExpression="col_5" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="col_6" HeaderText="Day 6"
                            UniqueName="col_6" SortExpression="col_6" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="col_7" HeaderText="Day 7"
                            UniqueName="col_7" SortExpression="col_7" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="PathwayItemID" UniqueName="PathwayItemID" SortExpression="PathwayItemID"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="PathwayID" UniqueName="PathwayID" SortExpression="PathwayID"
                            Visible="false" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ClinicalPathwayItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PathwayItemEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="False" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRiwayat" runat="server">
            <telerik:RadGrid ID="grdDiagnose" runat="server" OnNeedDataSource="grdDiagnose_NeedDataSource"
                OnUpdateCommand="grdDiagnose_UpdateCommand" OnDeleteCommand="grdDiagnose_DeleteCommand"
                OnInsertCommand="grdDiagnose_InsertCommand" AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="PathwayID, DiagnoseID" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="DiagnoseID" UniqueName="DiagnoseID" SortExpression="DiagnoseID"
                            HeaderStyle-Width="100px" HeaderText="Diagnose ID" />
                        <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnose Name" UniqueName="DiagnoseName"
                            SortExpression="DiagnoseName" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ClinicalPathwayDiagnoseItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PathwayDiagnoseItemEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="False" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
