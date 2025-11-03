<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="CasemixDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.CasemixDetail" %>

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
                        <td class="label">A L O S
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox runat="server" ID="txtALOS" Width="100px" MinValue="0"
                                MaxValue="10" NumberFormat-DecimalDigits="0" />
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
                    <tr>
                        <td class="label">Coverage Value (Est)
                        </td>
                        <td colspan="3">
                            <telerik:RadNumericTextBox runat="server" ID="txtClass1" Width="100px" />
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
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="MultiPage1" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Diagnose" PageViewID="pgRiwayat" />
            <telerik:RadTab runat="server" Text="Procedure" PageViewID="pgDetail" Selected="True" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="MultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgDetail" runat="server" Selected="true">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                OnUpdateCommand="grdList_UpdateCommand" OnDeleteCommand="grdList_DeleteCommand"
                ShowFooter="true" OnInsertCommand="grdList_InsertCommand" AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="PathwayID, ItemID" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="ItemGroupName" HeaderText="Item Group Name" UniqueName="ItemGroupName"
                            SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="PathwayID" UniqueName="PathwayID" SortExpression="PathwayID"
                            Visible="false" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="CasemixItemDetail.ascx" EditFormType="WebUserControl">
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
                    <EditFormSettings UserControlName="../ClinicalPathway/ClinicalPathwayDiagnoseItemDetail.ascx" EditFormType="WebUserControl">
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
