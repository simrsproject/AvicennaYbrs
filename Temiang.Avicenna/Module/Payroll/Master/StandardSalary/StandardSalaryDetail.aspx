<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="StandardSalaryDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.StandardSalaryDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr style="display: none">
            <td class="label">
                <asp:Label ID="lblStandardSalaryID" runat="server" Text="Standard Salary ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtStandardSalaryID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvStandardSalaryID" runat="server" ErrorMessage="Standard Salary ID required."
                    ValidationGroup="entry" ControlToValidate="txtStandardSalaryID" SetFocusOnError="True"
                    Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPositionGradeID" runat="server" Text="Position Grade Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPositionGradeID" runat="server" Width="304px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPositionGradeID_ItemDataBound"
                    OnItemsRequested="cboPositionGradeID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PositionGradeCode")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "PositionGradeName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 10 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPositionGradeID" runat="server" ErrorMessage="Position Grade Name required."
                    ValidationGroup="entry" ControlToValidate="cboPositionGradeID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900"
                    MaxDate="12/31/2999" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                    ValidationGroup="entry" ControlToValidate="txtValidFrom" SetFocusOnError="True"
                    Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900"
                    MaxDate="12/31/2999" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                    ValidationGroup="entry" ControlToValidate="txtValidTo" SetFocusOnError="True"
                    Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Faktor" PageViewID="pgvFaktor" Selected="true">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvFaktor" runat="server">
            <telerik:RadGrid ID="grdStandardSalaryFaktor" runat="server" OnNeedDataSource="grdStandardSalaryFaktor_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdStandardSalaryFaktor_UpdateCommand"
                OnDeleteCommand="grdStandardSalaryFaktor_DeleteCommand" OnInsertCommand="grdStandardSalaryFaktor_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="StandardSalaryFaktorID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="StandardSalaryFaktorID"
                            HeaderText="Standard Salary Faktor ID" UniqueName="StandardSalaryFaktorID" SortExpression="StandardSalaryFaktorID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="GradeServiceYear"
                            HeaderText="Grade Year" UniqueName="GradeServiceYear" SortExpression="GradeServiceYear"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="AmountSalary" HeaderText="Amount Salary"
                            UniqueName="AmountSalary" SortExpression="AmountSalary" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn/>    
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="StandardSalaryFaktorDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="StandardSalaryFaktorEditCommand">
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
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
