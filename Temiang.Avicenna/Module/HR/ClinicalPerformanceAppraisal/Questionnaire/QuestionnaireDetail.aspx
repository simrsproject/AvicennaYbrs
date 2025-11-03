<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="QuestionnaireDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.CPA.QuestionnaireDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Code</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtQuestionnaireCode" runat="server" Width="300px" MaxLength="20" /></td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvQuestionnaireCode" runat="server" ErrorMessage="Code required."
                                ValidationGroup="entry" ControlToValidate="txtQuestionnaireCode" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Form Name</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtQuestionnaireName" runat="server" Width="300px" TextMode="MultiLine" MaxLength="255" /></td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvQuestionnaireName" runat="server" ErrorMessage="Form Name required."
                                ValidationGroup="entry" ControlToValidate="txtQuestionnaireName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMinValue" runat="server" Text="Minimum Score"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtMinValue" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvMinValue" runat="server" ErrorMessage="Minimum Score required."
                                ControlToValidate="txtMinValue" SetFocusOnError="True" ValidationGroup="entry" Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMaxValue" runat="server" Text="Maximum Score"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtMaxValue" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvMaxValue" runat="server" ErrorMessage="Maximum Score required."
                                ControlToValidate="txtMaxValue" SetFocusOnError="True" ValidationGroup="entry" Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Questionnaire" PageViewID="pgQuestionnaire"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Conclusion" PageViewID="pgConclusion">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgQuestionnaire" runat="server" Selected="true">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AutoGenerateColumns="false" AllowPaging="true" PageSize="50"
                OnInsertCommand="grdList_InsertCommand" OnUpdateCommand="grdList_UpdateCommand" OnDeleteCommand="grdList_DeleteCommand">
                <MasterTableView DataKeyNames="QuestionnaireItemID" GroupLoadMode="Client" CommandItemDisplay="Top">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionCode" HeaderText="Code"
                            UniqueName="QuestionCode" SortExpression="QuestionCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionNo" HeaderText="No."
                            UniqueName="QuestionNo" SortExpression="QuestionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="QuestionGroupName" HeaderText="Question Group Name"
                            UniqueName="QuestionGroupName" SortExpression="QuestionGroupName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="QuestionName" HeaderText="Question Name"
                            UniqueName="QuestionName" SortExpression="QuestionName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsDetail" HeaderText="Detail"
                            UniqueName="IsDetail" SortExpression="IsDetail" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LoadScore" HeaderText="Load Score"
                            UniqueName="LoadScore" SortExpression="LoadScore" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="QuestionnaireItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="QuestionnaireItemDetailEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgConclusion" runat="server">
            <telerik:RadGrid ID="grdConclusion" runat="server" OnNeedDataSource="grdConclusion_NeedDataSource" AutoGenerateColumns="false" AllowPaging="true" PageSize="50"
                OnInsertCommand="grdConclusion_InsertCommand" OnUpdateCommand="grdConclusion_UpdateCommand" OnDeleteCommand="grdConclusion_DeleteCommand">
                <MasterTableView DataKeyNames="ConclusionID" GroupLoadMode="Client" CommandItemDisplay="Top">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ConclusionGrade" HeaderText="Grade"
                            UniqueName="ConclusionGrade" SortExpression="ConclusionGrade" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ConclusionGradeName" HeaderText="Grade Name"
                            UniqueName="ConclusionGradeName" SortExpression="ConclusionGradeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MinValue" HeaderText="Minimum Value"
                            UniqueName="MinValue" SortExpression="MinValue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MaxValue" HeaderText="Maximum Value"
                            UniqueName="MaxValue" SortExpression="MaxValue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="QuestionnaireConclusionDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="QuestionnaireConclusionDetailEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

</asp:Content>
