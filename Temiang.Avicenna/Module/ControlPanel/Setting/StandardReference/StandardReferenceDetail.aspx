<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="StandardReferenceDetail.aspx.cs" Inherits="Temiang.Avicenna.ControlPanel.Setting.StandardReferenceDetail"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStandardReferenceID" runat="server" Text="Standard Reference ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtStandardReferenceID" runat="server" Width="300px" MaxLength="30" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvStandardReferenceID" runat="server" ErrorMessage="Standard Reference ID required."
                                ValidationGroup="entry" ControlToValidate="txtStandardReferenceID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStandardReferenceName" runat="server" Text="Standard Reference Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtStandardReferenceName" runat="server" Width="300px" MaxLength="200"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvStandardReferenceName" runat="server" ErrorMessage="Standard Reference Name required."
                                ValidationGroup="entry" ControlToValidate="txtStandardReferenceName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStandardReferenceGroup" runat="server" Text="Standard Reference Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtStandardReferenceGroup" runat="server" Width="300px" MaxLength="30" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemLength" runat="server" Text="Item ID Max Length"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtItemLength" runat="server" Width="100px" NumberFormat-DecimalDigits="0"
                                Type="Number" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsHasCOA" runat="server" Text="Using COA Reference" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsUsedBySystem" runat="server" Text="Used By System" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="grdStandardReferenceItem" runat="server" AllowMultiRowSelection="true"
                    OnNeedDataSource="grdStandardReferenceItem_NeedDataSource" AutoGenerateColumns="False"
                    GridLines="None" OnUpdateCommand="grdStandardReferenceItem_UpdateCommand" OnDeleteCommand="grdStandardReferenceItem_DeleteCommand"
                    OnInsertCommand="grdStandardReferenceItem_InsertCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="ItemID" HeaderStyle-Width="100px" HeaderText="Item ID"
                                UniqueName="ItemID" SortExpression="ItemID">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" SortExpression="ItemName" HeaderStyle-Width="250px"
                                UniqueName="ItemName">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn DataField="NumericValue" HeaderText="Numeric Value" UniqueName="NumericValue" HeaderStyle-Width="100px"
                                SortExpression="NumericValue" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn DataField="Note" HeaderText="Note" SortExpression="Note"
                                UniqueName="Note">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ReferenceID" HeaderText="Reference ID" SortExpression="ReferenceID"
                                UniqueName="ReferenceID" HeaderStyle-Width="150px">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn DataField="ReferenceID" HeaderText="Back Color" UniqueName="BackColorTemplateColumn">
                                <ItemTemplate>
                                    <div style="width: 100%; background-color: <%#DataBinder.Eval(Container.DataItem,"ReferenceID")%>">
                                        <%#DataBinder.Eval(Container.DataItem,"ReferenceID")%>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridNumericColumn DataField="LineNumber" HeaderText="Line Number" UniqueName="LineNumber" HeaderStyle-Width="100px"
                                SortExpression="LineNumber" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}">
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn DataField="CoaCode" HeaderText="Coa Code" SortExpression="CoaCode"
                                UniqueName="CoaCode" HeaderStyle-Width="100px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CoaName" HeaderText="Coa Name" SortExpression="CoaName" HeaderStyle-Width="150px"
                                UniqueName="CoaName">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsUsedBySystem" HeaderText="System"
                                UniqueName="IsUsedBySystem" SortExpression="IsUsedBySystem" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn DataField="IsActive" HeaderStyle-Width="100px" HeaderText="Active"
                                SortExpression="IsActive" UniqueName="IsActive">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="StandardReferenceItemDetail.ascx" EditFormType="WebUserControl">
                        </EditFormSettings>
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
</asp:Content>
