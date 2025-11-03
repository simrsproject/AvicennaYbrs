<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="RemunerationPositionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.RemunerationPosition.RemunerationPositionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblWageStructureAndScalePositionID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtWageStructureAndScalePositionID" runat="server" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lbPersonID" runat="server" Text="Employee Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                OnItemsRequested="cboPersonID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Employee Name required."
                                ValidationGroup="entry" ControlToValidate="cboPersonID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                                ValidationGroup="entry" ControlToValidate="txtValidFrom" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmployeeWorkGroup" runat="server" Text="Work Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREmployeeWorkGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSREmployeeWorkGroup_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSREmployeeWorkGroup" runat="server" ErrorMessage="Work Group required."
                                ValidationGroup="entry" ControlToValidate="cboSREmployeeWorkGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmployeeWorkSubGroup" runat="server" Text="Work Sub Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREmployeeWorkSubGroup" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboSREmployeeWorkSubGroup_ItemDataBound"
                                OnItemsRequested="cboSREmployeeWorkSubGroup_ItemsRequested" OnSelectedIndexChanged="cboSREmployeeWorkSubGroup_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSREmployeeWorkSubGroup" runat="server" ErrorMessage="Work Sub Group required."
                                ValidationGroup="entry" ControlToValidate="cboSREmployeeWorkSubGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmployeeJobPosition" runat="server" Text="Job Position"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREmployeeJobPosition" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSREmployeeJobPosition_ItemDataBound"
                                OnItemsRequested="cboSREmployeeJobPosition_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfSREmployeeJobPosition" runat="server" ErrorMessage="Job Position required."
                                ValidationGroup="entry" ControlToValidate="cboSREmployeeJobPosition" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label"><asp:Label ID="lblBasePoint" runat="server" Text="Base Point (Calculation)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtBasePoint" runat="server" Width="200px" Height="50px" Font-Size="30px" NumberFormat-DecimalDigits="2" ReadOnly="True" 
                                BackColor="Gray" ForeColor="White"/>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label"><asp:Label ID="lblPoints" runat="server" Text="Points (Acknowledged)"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtPoints" runat="server" Width="200px" Height="51px" Font-Size="30px" NumberFormat-DecimalDigits="2" 
                                AutoPostBack="true" OnTextChanged="txtPoints_TextChanged"/>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionGradeID" runat="server" Text="Position Grade"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPositionGradeID" runat="server" Width="200px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPositionGradeID_ItemDataBound"
                                OnItemsRequested="cboPositionGradeID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "PositionGradeName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionGradeID" runat="server" ErrorMessage="Position Grade required."
                                ValidationGroup="entry" ControlToValidate="cboPositionGradeID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                            <asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
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
        <MasterTableView CommandItemDisplay="None" DataKeyNames="WageStructureAndScalePositionItemID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="WageStructureAndScalePositionItemID" UniqueName="WageStructureAndScalePositionItemID"
                    SortExpression="WageStructureAndScalePositionItemID" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SRWageStructureAndScaleType" UniqueName="SRWageStructureAndScaleType"
                    SortExpression="SRWageStructureAndScaleType" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="WageStructureAndScaleTypeName" HeaderText="Type" UniqueName="WageStructureAndScaleTypeName"
                    SortExpression="WageStructureAndScaleTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="WageStructureAndScaleName" HeaderText="Wage Structure And Scale" UniqueName="WageStructureAndScaleName"
                    SortExpression="WageStructureAndScaleName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="WageStructureAndScaleItemName" HeaderText="Wage Structure And Scale Item" UniqueName="WageStructureAndScaleItemName"
                    SortExpression="WageStructureAndScaleItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="LoadPoint" HeaderText="Load (%)" UniqueName="LoadPoint"
                    SortExpression="LoadPoint" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="BasePoint" HeaderText="Points" UniqueName="BasePoint"
                    SortExpression="BasePoint" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Points" HeaderText="Result (Load x Points)" UniqueName="Points"
                    SortExpression="Points" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="RemunerationPositionItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="RemunerationPositionItemDetailEditCommand">
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
