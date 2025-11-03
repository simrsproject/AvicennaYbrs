<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="QuestionnaireDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Questionnaire.QuestionnaireDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Code</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtQuestionnaireCode" runat="server" Width="300px" MaxLength="50" /></td>
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
                        <td class="label">Profession Group</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRProfessionGroup" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRProfessionGroup_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRProfessionGroup" runat="server" ErrorMessage="Profession Group required."
                                ValidationGroup="entry" ControlToValidate="cboSRProfessionGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Work Area</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRClinicalWorkArea" runat="server" Width="300px" EnableLoadOnDemand="True"
                                HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboSRClinicalWorkArea_ItemDataBound"
                                OnItemsRequested="cboSRClinicalWorkArea_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboSRClinicalWorkArea_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRClinicalWorkArea" runat="server" ErrorMessage="Work Area required."
                                ValidationGroup="entry" ControlToValidate="cboSRClinicalWorkArea" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Clinical Authority Level / Qualification</td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRClinicalAuthorityLevel" runat="server" Width="300px" EnableLoadOnDemand="True"
                                HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboSRClinicalAuthorityLevel_ItemDataBound"
                                OnItemsRequested="cboSRClinicalAuthorityLevel_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRClinicalAuthorityLevel" runat="server" ErrorMessage="Clinical Authority Level / Qualification required."
                                ValidationGroup="entry" ControlToValidate="cboSRClinicalAuthorityLevel" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
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
            <telerik:RadTab runat="server" Text="Guidelines" PageViewID="pgGuidance">
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
                            UniqueName="QuestionCode" SortExpression="QuestionCode" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionNo" HeaderText="No."
                            UniqueName="QuestionNo" SortExpression="QuestionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="QuestionName" HeaderText="Question Name"
                            UniqueName="QuestionName" SortExpression="QuestionName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="CredentialActionTypeName" HeaderText="Action Type"
                            UniqueName="CredentialActionTypeName" SortExpression="CredentialActionTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="CredentialQuestionLevelName" HeaderText="Question Level"
                            UniqueName="CredentialQuestionLevelName" SortExpression="CredentialQuestionLevelName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsDetail" HeaderText="Detail"
                            UniqueName="IsDetail" SortExpression="IsDetail" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
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
        <telerik:RadPageView ID="pgGuidance" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" valign="top">
                        <fieldset>
                            <legend>
                                <asp:Label ID="lblInfo1" runat="server" Text="INFO 1" Font-Bold="true"></asp:Label></legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtInfo1" runat="server" Width="100%" MaxLength="4000" TextMode="MultiLine" Height="200" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td width="50%" valign="top">
                        <fieldset>
                            <legend>
                                <asp:Label ID="lblInfo2" runat="server" Text="INFO 2" Font-Bold="true"></asp:Label></legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtInfo2" runat="server" Width="100%" MaxLength="4000" TextMode="MultiLine" Height="200" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td width="50%" valign="top">
                        <fieldset>
                            <legend>
                                <asp:Label ID="lblInfo3" runat="server" Text="INFO 3" Font-Bold="true"></asp:Label></legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtInfo3" runat="server" Width="100%" MaxLength="4000" TextMode="MultiLine" Height="200" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td width="50%" valign="top">
                        <fieldset>
                            <legend>
                                <asp:Label ID="lblInfo4" runat="server" Text="INFO 4" Font-Bold="true"></asp:Label></legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtInfo4" runat="server" Width="100%" MaxLength="4000" TextMode="MultiLine" Height="200" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

</asp:Content>
