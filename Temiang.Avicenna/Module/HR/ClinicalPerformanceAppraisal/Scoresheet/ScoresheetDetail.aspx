<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ScoresheetDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.CPA.ScoresheetDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblScoresheetNo" runat="server" Text="Scoresheet No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtScoresheetNo" runat="server" Width="160px" ReadOnly="true" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvScoresheetNo" runat="server" ErrorMessage="Scoresheet No required."
                                ValidationGroup="entry" ControlToValidate="txtScoresheetNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblScoringDate" runat="server" Text="Date" />
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtScoringDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvScoringDate" runat="server" ErrorMessage="Date required."
                                ValidationGroup="entry" ControlToValidate="txtScoringDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEvaluatorID" runat="server" Text="Evaluator Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboEvaluatorID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboEvaluatorID_ItemDataBound"
                                OnItemsRequested="cboEvaluatorID_ItemsRequested">
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
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvEvaluatorID" runat="server" ErrorMessage="Evaluator Name required."
                                ValidationGroup="entry" ControlToValidate="cboEvaluatorID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image22" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label"> 
                            <asp:Label ID="lblTotalScore" runat="server" Text="Total Score"></asp:Label>
                        </td>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="150px" Height="80px" Font-Size="40px" NumberFormat-DecimalDigits="0" ReadOnly="True" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblConclusionGrade" runat="server" Font-Size="40px"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;<asp:Label ID="lblConclusionSeparate" runat="server" Font-Size="40px"></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblConclusionGradeName" runat="server" Font-Size="40px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td>
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                            <asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <fieldset>
                    <legend>
                        <asp:Label ID="lblIdentity" runat="server" Text="IDENTITY"></asp:Label></legend>
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPersonID" runat="server" Text="Employee Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                    OnItemsRequested="cboPersonID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboPersonID_SelectedIndexChanged">
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
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Employee Name required."
                                    ValidationGroup="entry" ControlToValidate="cboPersonID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEmployeeNo" runat="server" Text="Employee No" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtEmployeeNo" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPlaceDOB" runat="server" Text="Place / Date Of Birth" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSREmploymentType" runat="server" Text="Employment Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSREmploymentType" runat="server" Width="300px" Enabled="false" />
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblREmploymentPermanentDate" runat="server" Text="Employment Permanent Date" />
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtREmploymentPermanentDate" runat="server" Width="100px" Enabled="false" />
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td width="50%" valign="top">
                <fieldset>
                    <legend>
                        <asp:Label ID="lblEmploymentData" runat="server" Text="EMPLOYMENT DATA"></asp:Label></legend>
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Section / Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                                    OnItemsRequested="cboServiceUnitID_ItemsRequested" Enabled="false">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPositionID" runat="server" Text="Position"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPositionID" runat="server" Width="300px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboPositionID_ItemDataBound"
                                    OnItemsRequested="cboPositionID_ItemsRequested" Enabled="false">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRProfessionGroup" runat="server" Text="Profession Group"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRProfessionGroup" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" Enabled="false" />
                            </td>
                            <td width="20">
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRClinicalWorkArea" runat="server" Text="Work Area"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRClinicalWorkArea" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" Enabled="false" />
                            </td>
                            <td width="20">
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRClinicalAuthorityLevel" runat="server" Text="Clinical Authority Level"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRClinicalAuthorityLevel" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" Enabled="false" />
                            </td>
                            <td width="20">
                            </td>
                            <td />
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Scoresheet" PageViewID="pgScoresheet"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Conclusion" PageViewID="pgConclusion">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgScoresheet" runat="server" Selected="true">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display:none">
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblQuestionnaireID" runat="server" Text="Form Name" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboQuestionnaireID" runat="server" Width="300px" EnableLoadOnDemand="True"
                                        HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboQuestionnaireID_ItemDataBound"
                                        OnItemsRequested="cboQuestionnaireID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboQuestionnaireID_SelectedIndexChanged">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "QuestionnaireName")%>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvQuestionnaireID" runat="server" ErrorMessage="Form Name required."
                                        ValidationGroup="entry" ControlToValidate="cboQuestionnaireID" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top"></td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdSheet" runat="server" OnNeedDataSource="grdSheet_NeedDataSource"
                OnItemDataBound="grdSheet_ItemDataBound" OnItemCreated="grdSheet_ItemCreated" AutoGenerateColumns="False" GridLines="None">
                <MasterTableView DataKeyNames="QuestionnaireItemID" CommandItemDisplay="None" ShowHeader="True">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionnaireItemID" HeaderText="ID"
                            UniqueName="QuestionnaireItemID" SortExpression="QuestionnaireItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionCode" HeaderText="Code"
                            UniqueName="QuestionCode" SortExpression="QuestionCode" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="QuestionNo" HeaderText="No."
                            UniqueName="QuestionNo" SortExpression="QuestionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="500px" HeaderText="Question Name" UniqueName="lblQuestionName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblQuestionName" runat="server" Text='<%# GetQuestionName(DataBinder.Eval(Container.DataItem, "QuestionGroupName"), DataBinder.Eval(Container.DataItem, "QuestionName")) %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Score" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" UniqueName="txtScore">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtScore" runat="server" NumberFormat-DecimalDigits="0"
                                    Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Score")) %>'
                                    MinValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "MinValue")) %>'
                                    MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "MaxValue")) %>'
                                    Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'
                                    Width="90px">
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Score" HeaderText="Score" UniqueName="Score" SortExpression="Score"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LoadScore" HeaderText="Load Score" UniqueName="LoadScore" SortExpression="LoadScore"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TotalScore" HeaderText="Total Score" UniqueName="TotalScore" SortExpression="TotalScore"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />

                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="IsDetail" HeaderText="IsDetail"
                            UniqueName="IsDetail" SortExpression="IsDetail" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="False" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgConclusion" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 100%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtConclusionNotes" runat="server" Width="95%" TextMode="MultiLine" Height="400px"/>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvConclusionNotes" runat="server" ErrorMessage="Conclusion required."
                                        ValidationGroup="entry" ControlToValidate="txtConclusionNotes" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top"></td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>

