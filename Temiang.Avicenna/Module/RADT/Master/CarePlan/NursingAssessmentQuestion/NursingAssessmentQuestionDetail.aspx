<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="NursingAssessmentQuestionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.NursingCare.Master.NursingAssessmentQuestionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr valign="top">
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblQuestionID" runat="server" Text="Assessment ID (auto)"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtQuestionID" runat="server" Width="100px" MaxLength="10" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label11" runat="server" Text="Related Question"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboQuestion" Width="300px" AutoPostBack="True"
                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                OnItemDataBound="cboQuestion_ItemDataBound" 
                                OnItemsRequested="cboQuestion_ItemsRequested"
                                OnSelectedIndexChanged="cboQuestion_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# (DataBinder.Eval(Container.DataItem, "QuestionID") == null) ?
                                            string.Empty : string.Format("<b>{0}</b><br />ID:{1}, Type:{2}", 
                                            DataBinder.Eval(Container.DataItem, "QuestionText").ToString(),
                                            DataBinder.Eval(Container.DataItem, "QuestionID").ToString(),
                                            DataBinder.Eval(Container.DataItem, "SRAnswerType").ToString())%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 30 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblQuestionText" runat="server" Text="Assessment Text"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtQuestionText" runat="server" Width="300px" MaxLength="500"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvQuestionText" runat="server" ErrorMessage="Assessment text required."
                                ValidationGroup="entry" ControlToValidate="txtQuestionText" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Assessment Question Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRAnswerType" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRAnswerType" runat="server" ErrorMessage="Type question required."
                                ValidationGroup="entry" ControlToValidate="cboSRAnswerType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Answer Decimal Digit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox runat="server" ID="txtAnswerDecimalDigit" Width="100px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td colspan="3">
                            
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
                <table border="0">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Answer Prefix"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAnswerPrefix" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Answer Suffix"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAnswerSuffix" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label12" runat="server" Text="Answer Selection"></asp:Label>
                        </td>
                        <td colspan="3">
                            <telerik:RadGrid ID="gridAnswerSelection" runat="server"
                                AutoGenerateColumns="False" GridLines="None">
                                <MasterTableView CommandItemDisplay="None" DataKeyNames="QuestionAnswerSelectionLineID">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionAnswerSelectionLineID" HeaderText="ID"
                                            UniqueName="QuestionAnswerSelectionLineID" SortExpression="QuestionAnswerSelectionLineID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn DataField="QuestionAnswerSelectionLineText" HeaderText="Text" UniqueName="QuestionAnswerSelectionLineText"
                                            SortExpression="QuestionAnswerSelectionLineText" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />   
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Formula"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFormula" runat="server" Width="300px" MaxLength="300" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Checked="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                    <tr style="display:none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsMandatory" runat="server" Text="Mandatory" Checked="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label9" runat="server" Text="Equivalent Question"></asp:Label>
                        </td>
                        <td width="500">
                            <telerik:RadComboBox runat="server" ID="cboEquivalentAssessmentID" Width="300px" AutoPostBack="True"
                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                OnItemDataBound="cboEquivalentAssessmentID_ItemDataBound" 
                                OnItemsRequested="cboEquivalentAssessmentID_ItemsRequested"
                                OnSelectedIndexChanged="cboEquivalentAssessmentID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# (DataBinder.Eval(Container.DataItem, "QuestionID") == null) ?
                                            string.Empty : string.Format("<b>{0}</b><br />ID:{1}, Type:{2}, Question:{0}", 
                                            DataBinder.Eval(Container.DataItem, "QuestionText").ToString(),
                                            DataBinder.Eval(Container.DataItem, "QuestionID").ToString(),
                                            DataBinder.Eval(Container.DataItem, "SRAnswerType").ToString())%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 30 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                            <asp:CheckBox ID="chkIsCopyTemplate" runat="server" Text="As Copy Template" OnCheckedChanged="chkIsCopyTemplate_CheckedChanged" AutoPostBack="false" Width="120" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Nursing Diagnosa" PageViewID="pgvNursingDiagnosaSetting" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Related Question and Form" PageViewID="pgvRelated" Selected="true">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvNursingDiagnosaSetting" runat="server">
            <telerik:RadGrid ID="grdNursingAssessmentDiagnosa" runat="server" 
                OnNeedDataSource="grdNursingAssessmentDiagnosa_NeedDataSource"
                OnItemDataBound="grdNursingAssessmentDiagnosa_ItemDataBound"
                AutoGenerateColumns="False" GridLines="None" 
                OnUpdateCommand="grdNursingAssessmentDiagnosa_UpdateCommand"
                OnDeleteCommand="grdNursingAssessmentDiagnosa_DeleteCommand" 
                OnInsertCommand="grdNursingAssessmentDiagnosa_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="NursingDiagnosaID" HeaderText="Diagnosa ID"
                            UniqueName="NursingDiagnosaID" SortExpression="NursingDiagnosaID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="NsDiagnosaPrefixName" HeaderText="Prefix" UniqueName="NsDiagnosaPrefixName"
                            SortExpression="NsDiagnosaPrefixName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="NursingDiagnosaName" HeaderText="Diagnosa Name" UniqueName="NursingDiagnosaName"
                            SortExpression="NursingDiagnosaName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                        <telerik:GridBoundColumn DataField="NsDiagnosaSuffixName" HeaderText="Suffix" UniqueName="NsDiagnosaSuffixName"
                            SortExpression="NsDiagnosaSuffixName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="AgeInMonthStart" HeaderText="Age Start" UniqueName="AgeInMonthStart"
                            SortExpression="AgeInMonthStart" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridTemplateColumn UniqueName="AgeStartFormatted" HeaderStyle-Width="80px" HeaderText="Age Start" 
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", 
                                    ((System.Convert.ToInt32(Eval("AgeInMonthStart")) % 12) == 0 ? (System.Convert.ToInt32(Eval("AgeInMonthStart")) / 12).ToString() : (System.Convert.ToInt32(Eval("AgeInMonthStart"))).ToString()),
                                    ((System.Convert.ToInt32(Eval("AgeInMonthStart")) % 12) == 0 ? "Years" : "Months"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="AgeInMonthEnd" HeaderText="Age End" UniqueName="AgeInMonthEnd"
                            SortExpression="AgeInMonthEnd" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridTemplateColumn UniqueName="AgeEndFormatted" HeaderStyle-Width="80px" HeaderText="Age End" 
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}",
                                    ((System.Convert.ToInt32(Eval("AgeInMonthEnd")) % 12) == 0 ? (System.Convert.ToInt32(Eval("AgeInMonthEnd")) / 12).ToString() : (System.Convert.ToInt32(Eval("AgeInMonthEnd"))).ToString()),
                                    ((System.Convert.ToInt32(Eval("AgeInMonthEnd")) % 12) == 0 ? "Years" : "Months"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="30px" DataField="Sex" HeaderText="Sex" UniqueName="Sex"
                            SortExpression="Sex" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Operand" HeaderText="Operand" UniqueName="Operand"
                            SortExpression="Operand" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" /> 
                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Checked" UniqueName="CheckValue">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblChecked" runat="server" Text='<%#((bool)Eval("CheckValue"))?"Selected":""%>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="AcceptedText" HeaderText="Text" UniqueName="AcceptedText"
                            SortExpression="AcceptedText" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="AcceptedNum" HeaderText="Value" UniqueName="AcceptedNum"
                            SortExpression="AcceptedNum" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="AcceptedNum2" HeaderText="Value 2" UniqueName="AcceptedNum2"
                            SortExpression="AcceptedNum2" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="NsMandatoryLevelName" HeaderText="Assessment Level" UniqueName="NsMandatoryLevelName"
                            SortExpression="NsMandatoryLevelName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="NursingAssessmentDiagnosaDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="NursingAssessmentDiagnosaEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvRelated" runat="server">
            <telerik:RadGrid ID="gridQuestionForm" runat="server" OnNeedDataSource="gridQuestionForm_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="QuestionFormID, QuestionID">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="QuestionFormID" HeaderText="Question Form ID"
                            UniqueName="QuestionFormID" SortExpression="QuestionFormID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Question Form Name" UniqueName="QuestionFormName"
                            SortExpression="QuestionFormName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />  
                        <telerik:GridBoundColumn DataField="QuestionID" HeaderText="Question ID" UniqueName="QuestionID"
                            SortExpression="QuestionID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                        <telerik:GridBoundColumn DataField="RelatedQuestion" HeaderText="Question" UniqueName="RelatedQuestion"
                            SortExpression="RelatedQuestion" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                        <telerik:GridBoundColumn DataField="SRAnswerType" HeaderText="Answer Type" UniqueName="SRAnswerType"
                            SortExpression="SRAnswerType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
