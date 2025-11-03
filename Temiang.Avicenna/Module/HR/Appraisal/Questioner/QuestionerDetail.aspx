<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="QuestionerDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.QuestionerDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Questionnaire No</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtQuestionerNo" runat="server" Width="100px" MaxLength="50" /></td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvQuestionerNo" runat="server" ErrorMessage="Questioner No required."
                                ValidationGroup="entry" ControlToValidate="txtQuestionerNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Questionnaire Name</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtQuestionerName" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500" /></td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvQuestionerName" runat="server" ErrorMessage="Questioner Name required."
                                ValidationGroup="entry" ControlToValidate="txtQuestionerName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr runat="server" id="trAppraisalType">
                        <td class="label">Appraisal Type</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRAppraisalType" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRAppraisalType_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRAppraisalType" runat="server" ErrorMessage="Appraisal Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRAppraisalType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Period Year</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="100px" MaxLength="4" /></td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPeriodYear" runat="server" ErrorMessage="Period Year required."
                                ValidationGroup="entry" ControlToValidate="txtPeriodYear" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr runat="server" id="trLoad">
                        <td class="label">Load (%)</td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtLoad" Type="Percent" MinValue="0" MaxValue="100" runat="server" Width="100px" /></td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvLoad" runat="server" ErrorMessage="Load (%) required."
                                ValidationGroup="entry" ControlToValidate="txtLoad" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Notes</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" MaxLength="4000" /></td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr runat="server" id="trScoringRecapitulation">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsScoringRecapitulation" runat="server" Text="Scoring Recapitulation" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblAppraisalTypeNote" runat="server" ForeColor="Red" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Questionnaire" PageViewID="pgvQuestioner" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Guidelines" PageViewID="pgvRating">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvQuestioner" runat="server">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AutoGenerateColumns="false" AllowPaging="true" PageSize="50"
                OnInsertCommand="grdList_InsertCommand" OnUpdateCommand="grdList_UpdateCommand" OnDeleteCommand="grdList_DeleteCommand">
                <MasterTableView DataKeyNames="QuestionerItemID" GroupLoadMode="Client" CommandItemDisplay="Top">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionCode" HeaderText="Code"
                            UniqueName="QuestionCode" SortExpression="QuestionCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="QuestionGroupName" HeaderText="Question Group"
                            UniqueName="QuestionGroupName" SortExpression="QuestionGroupName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="QuestionName" HeaderText="Question Name"
                            UniqueName="QuestionName" SortExpression="QuestionName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Description"
                            UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Target" HeaderText="Target"
                            UniqueName="Target" SortExpression="TargetText" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Achievements" HeaderText="Achievements"
                            UniqueName="Achievements" SortExpression="Achievements" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Rating" HeaderText="Rating (%)"
                            UniqueName="Rating" SortExpression="Rating" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Benchmark" HeaderText="Benchmark (%)"
                            UniqueName="Benchmark" SortExpression="Benchmark" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="MinValue" HeaderText="Min Value"
                            UniqueName="MinValue" SortExpression="MinValue" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="MaxValue" HeaderText="Max Value"
                            UniqueName="MaxValue" SortExpression="MaxValue" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="QuestionerItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="QuestionerItemDetailEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvRating" runat="server">
            <telerik:RadGrid ID="grdRating" runat="server" OnNeedDataSource="grdRating_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdRating_UpdateCommand"
                OnDeleteCommand="grdRating_DeleteCommand" OnInsertCommand="grdRating_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="RatingID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RatingID" HeaderText="ID"
                            UniqueName="RatingID" SortExpression="RatingID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QuestionerID" HeaderText="QuestionerID"
                            UniqueName="QuestionerID" SortExpression="QuestionerID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RatingCode" HeaderText="Code"
                            UniqueName="RatingCode" SortExpression="RatingCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="RatingName" HeaderText="Rating Name" UniqueName="RatingName"
                            SortExpression="RatingName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RatingValue" HeaderText="Rating Value"
                            UniqueName="RatingValue" SortExpression="RatingValue" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RatingValueMin" HeaderText="Min Value (%)"
                            UniqueName="RatingValueMin" SortExpression="RatingValueMin" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RatingValueMax" HeaderText="Max Value (%)"
                            UniqueName="RatingValueMax" SortExpression="RatingValueMax" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="QuestionRatingDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="QuestionRatingEditCommand">
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
