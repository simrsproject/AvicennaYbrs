<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NutritionCareStandardEvaluation.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare.NutritionCareStandardEvaluation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="NutritionCareDiagnoseTransDT" />
    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="NutritionCareDiagnoseTransDT"
        ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            function openPrintDialog() {
                var oWndX = $find("<%= winDlg.ClientID %>");
                oWndX.SetUrl("NutritionCareStandardPickImplementation.aspx?idL10=<%= idL10 %>&");
                oWndX.show();
                oWndX.maximize();
            }

            function win_OnClientClose(oWndX, args) {
                //alert(oWndX.argument.dataDS);
                if (oWndX.argument == undefined || oWndX.argument == null) {
                } else {
                    if (oWndX.argument.result == undefined || oWndX.argument.result == null) {
                    } else if (oWndX.argument.result == 'OK') {
                        $find('<%=txtMonitoringEvaluation.ClientID %>').set_value(decodeURI(oWndX.argument.dataDS));
                    }
            }
            oWndX = null;
                //alert("post back");
        }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindow ID="winDlg" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false" OnClientClose="win_OnClientClose"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <span style="font-size: 1.5em;"><%= FullDiagnosaName %></span>
    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="gridListEvaluasi" runat="server" AutoGenerateColumns="false"
        GridLines="None" OnNeedDataSource="gridListEvaluasi_NeedDataSource"
        OnItemDataBound="gridListEvaluasi_ItemDataBound"
        OnDeleteCommand="gridListEvaluasi_DeleteCommand">
        <MasterTableView ClientDataKeyNames="ID" HierarchyLoadMode="ServerBind"
            HierarchyDefaultExpanded="true" ExpandCollapseColumn-Display="false" DataKeyNames="ID"
            GroupLoadMode="Client" Name="MainTable" CommandItemDisplay="None">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="35px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="ExecuteDateTime" HeaderText="Date"
                    DataFormatString="{0:MM/dd/yyyy HH:mm}" DataType="System.DateTime"
                    SortExpression="ExecuteDateTime" UniqueName="ExecuteDateTime">
                    <HeaderStyle HorizontalAlign="Left" Width="150" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ME" HeaderText="Monitoring & Evaluation"
                    SortExpression="ME" UniqueName="ME">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings
                UserControlName="NutritionCareStandardDetailEvaluation.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="NutritionCareStandardDetailEvaluationCommand">
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
    <table width="100%">
        <tr>
            <td style="width: 50%;" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Date Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDateTimePicker ID="txtDateTimeImplementation" runat="server" AutoPostBackControl="None">
                                <DateInput ID="DateInput1" runat="server"
                                    DisplayDateFormat="dd/MM/yyyy HH:mm"
                                    DateFormat="dd/MM/yyyy HH:mm">
                                </DateInput>
                                <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm"></TimeView>
                            </telerik:RadDateTimePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvDateTimeImplementation" runat="server" ErrorMessage="Nutrition care implementation date time required."
                                ValidationGroup="NutritionCareDiagnoseTransDT" ControlToValidate="txtDateTimeImplementation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTerminoloyID" runat="server" Text="Monitoring & Evaluation"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:HiddenField ID="hfID" runat="server" />
                            <asp:HiddenField ID="hfTmpTerminologyID" runat="server" />
                            <telerik:RadTextBox ID="txtMonitoringEvaluation" runat="server" Width="300px" TextMode="MultiLine" Height="450px" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchNote" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                ToolTip="Pick from nutrition notes" OnClientClick="openPrintDialog();return false;" Visible="false" />
                        </td>
                        <td></td>
                    </tr>

                </table>
            </td>
            <td style="width: 50%;" valign="top">
                <div style="width: 100%; height: 500px; overflow: scroll">
                    <telerik:RadGrid ID="gridListRencana" runat="server" AutoGenerateColumns="false"
                        GridLines="None" OnNeedDataSource="gridListRencana_NeedDataSource" OnItemDataBound="gridListRencana_ItemDataBound">
                        <MasterTableView ClientDataKeyNames="TerminologyID"
                            HierarchyDefaultExpanded="true" ExpandCollapseColumn-Display="false" DataKeyNames="TerminologyID"
                            GroupLoadMode="Client">
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="DomainName" HeaderText=" " HeaderValueSeparator=" "></telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="DomainSeqNo" SortOrder="Ascending"></telerik:GridGroupByField>
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Select" UniqueName="ChkBox" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="defaultChkBox" runat="server"
                                            Checked='<%#!Eval("TransTerminologyID").ToString().Equals(string.Empty)%>'></asp:CheckBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Detail" UniqueName="IsDetail" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsDetail" runat="server" Checked='<%#Eval("IsDetail")%>'></asp:CheckBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="TransTerminologyID" UniqueName="IsTransTerminologyID" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkTransTerminologyID" runat="server"
                                            Checked='<%#!Eval("TransTerminologyID").ToString().Equals(string.Empty)%>'></asp:CheckBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="TerminologyID" HeaderText="Code" SortExpression="TerminologyID"
                                    UniqueName="TerminologyID">
                                    <HeaderStyle HorizontalAlign="Left" Width="100" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TerminologyNameEdited" HeaderText="Intervention" SortExpression="TerminologyNameEdited"
                                    UniqueName="TerminologyNameEdited">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Intervention" UniqueName="TerminologyNameEditedTxt" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txtTerminologyName" runat="server" Text='<%#Eval("TerminologyNameEdited")%>'
                                            Width="100%" MaxLength="255" onkeyup="AutoRes(this);" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <%--<telerik:GridTemplateColumn HeaderText="Intervention" UniqueName="TerminologyNameEdited">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txtTerminologyName" runat="server" Text='<%#Eval("TerminologyNameEdited")%>'
                                            Width="100%" MaxLength="255" TextMode="MultiLine" Rows="1" onkeyup="AutoRes(this);" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
                                <telerik:GridBoundColumn DataField="TerminologyName" HeaderText="NIC" SortExpression="TerminologyName"
                                    UniqueName="TerminologyName" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TerminologyParentID" HeaderText="Parent"
                                    SortExpression="TerminologyParentID" UniqueName="TerminologyParentID"
                                    Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
