<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="SalaryScaleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryScaleDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSalaryScaleID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtSalaryScaleID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSalaryScaleID" runat="server" ErrorMessage="ID required."
                                ValidationGroup="entry" ControlToValidate="txtSalaryScaleID" SetFocusOnError="True"
                                Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalaryScaleCode" runat="server" Text="Salary Scale Code"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSalaryScaleCode" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSalaryScaleCode" runat="server" ErrorMessage="Salary Scale Code required."
                                ValidationGroup="entry" ControlToValidate="txtSalaryScaleCode" SetFocusOnError="True"
                                Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalaryScaleName" runat="server" Text="Salary Scale Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSalaryScaleName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSalaryScaleName" runat="server" ErrorMessage="Salary Scale Name required."
                                ValidationGroup="entry" ControlToValidate="txtSalaryScaleName" SetFocusOnError="True"
                                Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmploymentType" runat="server" Text="Employment Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSREmploymentType" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSREmploymentType_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSREmploymentType" runat="server" ErrorMessage="Employment Type required."
                                ValidationGroup="entry" ControlToValidate="cboSREmploymentType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionGradeID" runat="server" Text="Position Grade"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPositionGradeID" runat="server" Width="300px" EnableLoadOnDemand="true"
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
                            <asp:RequiredFieldValidator ID="rfvPositionGradeID" runat="server" ErrorMessage="Position Grade required."
                                ValidationGroup="entry" ControlToValidate="cboPositionGradeID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRProfessionGroup" runat="server" Text="Profession Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRProfessionGroup" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRProfessionGroup" runat="server" ErrorMessage="Profession Group required."
                                ValidationGroup="entry" ControlToValidate="cboSRProfessionGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREducationGroup" runat="server" Text="Education Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSREducationGroup" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSREducationGroup" runat="server" ErrorMessage="Education Group required."
                                ValidationGroup="entry" ControlToValidate="cboSREducationGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="255" TextMode="MultiLine" />
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
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Factor" PageViewID="pgvFactor" Selected="true">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvFactor" runat="server">
            <telerik:RadGrid ID="grdSalaryScaleFactor" runat="server" OnNeedDataSource="grdSalaryScaleFactor_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdSalaryScaleFactor_UpdateCommand"
                OnDeleteCommand="grdSalaryScaleFactor_DeleteCommand" OnInsertCommand="grdSalaryScaleFactor_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SalaryScaleFactorID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SalaryScaleFactorID"
                            HeaderText="ID" UniqueName="SalaryScaleFactorID" SortExpression="SalaryScaleFactorID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="ValidFrom"
                            HeaderText="Valid From" UniqueName="ValidFrom" SortExpression="ValidFrom"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Amount" HeaderText="Amount"
                            UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="SalaryScaleFactorDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="SalaryScaleFactorEditCommand">
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
