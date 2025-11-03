<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="WageStructureAndScalePointsDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.WageStructureAndScalePointsDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblWageStructureAndScaleID" runat="server" Text="WageStructureAndScaleID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtWageStructureAndScaleID" runat="server" Width="300px" MinValue="-1" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRWageStructureAndScaleType" runat="server" Text="Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRWageStructureAndScaleType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRWageStructureAndScaleType" runat="server" ErrorMessage="Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRWageStructureAndScaleType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblWageStructureAndScaleCode" runat="server" Text="Code"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtWageStructureAndScaleCode" runat="server" Width="300px" MaxLength="10" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvWageStructureAndScaleCode" runat="server" ErrorMessage="Code required."
                                ValidationGroup="entry" ControlToValidate="txtWageStructureAndScaleCode" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblWageStructureAndScaleName" runat="server" Text="Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtWageStructureAndScaleName" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvWageStructureAndScaleName" runat="server" ErrorMessage="Name required."
                                ValidationGroup="entry" ControlToValidate="txtWageStructureAndScaleName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItem_UpdateCommand"
        OnDeleteCommand="grdItem_DeleteCommand" OnInsertCommand="grdItem_InsertCommand">
        <HeaderContextMenu>
            <CollapseAnimation Duration="200" Type="OutQuint" />
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="WageStructureAndScaleItemID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="WageStructureAndScaleItemID"
                    HeaderText="ID" UniqueName="WageStructureAndScaleItemID" SortExpression="WageStructureAndScaleItemID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRWageStructureAndScaleItem"
                    HeaderText="ID" UniqueName="SRWageStructureAndScaleItem" SortExpression="SRWageStructureAndScaleItem"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="WageStructureAndScaleItemName"
                    HeaderText="Item Name" UniqueName="WageStructureAndScaleItemName" SortExpression="WageStructureAndScaleItemName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Points" HeaderText="Points" UniqueName="Points"
                    SortExpression="Points" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="WageStructureAndScalePointsItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="WageStructureAndScalePointsItemDetailEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
            <CollapseAnimation Duration="200" Type="OutQuint" />
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
