<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="GuarantorInfo.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.GuarantorInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend>GUARANTOR INFORMATION</legend>
                    <table width="100%">
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <asp:Panel runat="server" ID="pnlEmployeeInfo">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblGuarSRRelationship" runat="server" Text="Relation"></asp:Label>
                                            </td>
                                            <td class="entry2Column">
                                                <telerik:RadComboBox ID="cboGuarSRRelationship" runat="server" Width="304px" />
                                            </td>
                                            <td width="20">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblInsuranceID" runat="server" Text="Insurance ID"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtInsuranceID" runat="server" Width="300px" MaxLength="50" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            SEP No (BPJS)
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtSepNo" runat="server" Width="300px" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblGuarIDCardNo" runat="server" Text="Guarantor Card No"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtGuarIDCardNo" runat="server" Width="300px" MaxLength="50" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="label">
                                            <asp:Label ID="lblRegistrationNo" runat="server" Text="RegistrationNo"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="50" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="- GUARANTOR UPDATE HISTORY -"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="grdHistoryUpdateGuarantor" runat="server" OnNeedDataSource="grdHistoryUpdateGuarantor_NeedDataSource"
                                                AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="5"
                                                AllowSorting="False">
                                                <HeaderContextMenu>
                                                </HeaderContextMenu>
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="FromGuarantorID, ToGuarantorID, LastUpdateDateTime"
                                                    GroupLoadMode="Client">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Date & Time"
                                                            UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" DataType="System.DateTime"
                                                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="FromGuarantorName" HeaderText="From" UniqueName="FromGuarantorName"
                                                            SortExpression="FromGuarantorName">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ToGuarantorName" HeaderText="To" UniqueName="ToGuarantorName"
                                                            SortExpression="ToGuarantorName">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="Update By" UniqueName="LastUpdateByUserID"
                                                            SortExpression="LastUpdateByUserID" HeaderStyle-Width="120px">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <FilterMenu>
                                                </FilterMenu>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Resizing AllowColumnResize="True" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <legend>C O B</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="grdRegistrationGuarantor" runat="server" OnNeedDataSource="grdRegistrationGuarantor_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdRegistrationGuarantor_UpdateCommand"
                                    OnDeleteCommand="grdRegistrationGuarantor_DeleteCommand" OnInsertCommand="grdRegistrationGuarantor_InsertCommand">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="RegistrationNo,GuarantorID">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="GuarantorID" HeaderText="ID" UniqueName="GuarantorID"
                                                SortExpression="GuarantorID">
                                                <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                                                SortExpression="GuarantorName">
                                                <HeaderStyle HorizontalAlign="Left" Width="250px"/>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PlafondAmount" HeaderText="Plafond Amount" UniqueName="PlafondAmount"
                                                SortExpression="PlafondAmount" DataFormatString="{0:n2}">
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                                SortExpression="Notes">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings UserControlName="../../../RADT/Registration/RegistrationGuarantorDetail.ascx"
                                            EditFormType="WebUserControl">
                                            <EditColumn UniqueName="RegistrationGuarantorEditCommand">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <FilterMenu>
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
