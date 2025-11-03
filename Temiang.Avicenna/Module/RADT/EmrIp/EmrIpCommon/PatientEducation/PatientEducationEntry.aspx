<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogHistEntry.Master" AutoEventWireup="true"
    CodeBehind="PatientEducationEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.PatientEducationEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphList" runat="server">
    <telerik:RadCodeBlock runat="server" ID="cb">
        <style>
            #educationLine {
                font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
                border-collapse: collapse;
                width: 100%;
            }

                #educationLine td, #educationLine th {
                    border: 1px solid #a9a9a9;
                    padding: 4px;
                }

                #educationLine tr:nth-child(even) {
                    background-color: #f2f2f2;
                }

                #educationLine tr:hover {
                    background-color: #ddd;
                }

                #educationLine th {
                    padding-top: 6px;
                    padding-bottom: 6px;
                    text-align: center;
                    background-color: #4CAF50;
                    color: white;
                }

            .ColumnSign {
                float: left;
                width: 50%;
            }
            /* Clear floats after the columns */
            .RowSign:after {
                content: "";
                display: table;
                clear: both;
            }
        </style>
        <script type="text/javascript" language="javascript">;

            function editSignature1() {
                var mod = 'edit';
                var imgId = '<%=fmImage.ClientID %>';
                var txtId = '<%=hdnImage1.ClientID %>';
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=' + mod + '&imgId=' + imgId + '&txtId=' + txtId;
                var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }

            function editSignature2() {
                var mod = 'edit';
                var imgId = '<%=psImage.ClientID %>';
                var txtId = '<%=hdnImage2.ClientID %>';
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=' + mod + '&imgId=' + imgId + '&txtId=' + txtId;
                var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }

            function winImage_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById(arg.txtId).value = arg.image;
                    var img = document.getElementById(arg.imgId);
                    img.setAttribute('src', "data:image/Png;base64," + arg.image);
                }
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.command == 'rebind') {
                    __doPostBack("<%= grdPatientEducationHist.UniqueID %>", 'rebind');
                }
            }
        </script>
    </telerik:RadCodeBlock>


    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
        ID="winImage" />

    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winProcess"
        OnClientClose="onClientClose">
    </telerik:RadWindow>

    <fieldset>
        <legend>Education History</legend>
        <telerik:RadGrid ID="grdPatientEducationHist" runat="server" OnNeedDataSource="grdPatientEducationHist_NeedDataSource"
            AutoGenerateColumns="False"
            OnItemCommand="grdPatientEducationHist_ItemCommand">
            <MasterTableView DataKeyNames="SeqNo, RegistrationNo" ClientDataKeyNames="SeqNo, RegistrationNo">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnView" runat="server" CommandName="View" ToolTip='View'>
                            <img src="../../../../../Images/Toolbar/views16.png" border="0" alt=""/>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="PrintLabel1" HeaderStyle-Width="30px"
                        ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnPrintLabel1" runat="server" CommandName="PrintLabel1"
                                ToolTip='Print Label 1' CommandArgument='<%# Eval("RegistrationNo")+";"+ Eval("SeqNo")%>'>
                                    <img src="../../../../../Images/Toolbar/print16.png" border="0" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridNumericColumn DataField="SeqNo" UniqueName="SeqNo" HeaderText="No" HeaderStyle-Width="50px" />
                    <telerik:GridDateTimeColumn DataField="EducationDateTime" UniqueName="EducationDateTime" HeaderText="Date Time" HeaderStyle-Width="100px" />
                    <telerik:GridTemplateColumn UniqueName="Educator" HeaderText="Educator" HeaderStyle-Width="200px">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "EducationByUserName")%><br />
                            (<%# DataBinder.Eval(Container.DataItem, "SRUserTypeName")%>)
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn UniqueName="Education" HeaderText="Education" HeaderStyle-Width="400px">
                        <ItemTemplate>
                            <%#PatientEducationLineHtml(Container) %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="SRPatientEducationProblemName" UniqueName="SRPatientEducationProblemName" HeaderText="Problem" HeaderStyle-Width="200px" />
                    <telerik:GridBoundColumn DataField="SRPatientEducationMethodName" UniqueName="SRPatientEducationMethodName" HeaderText="Method" HeaderStyle-Width="200px" />

                    <telerik:GridTemplateColumn UniqueName="Recipient" HeaderText="Recipient" HeaderStyle-Width="200px">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "RecipientName")%><br />
                            (<%# DataBinder.Eval(Container.DataItem, "SRPatientEducationRecipientName")%>)
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="SRPatientEducationEvaluationName" UniqueName="SRPatientEducationEvaluationName" HeaderText="Evaluation" HeaderStyle-Width="200px" />
                    <telerik:GridBoundColumn DataField="Duration" UniqueName="Duration" HeaderText="Duration" HeaderStyle-Width="100px" />

                    <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="False">
                <Selecting AllowRowSelect="False" />
                <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
            </ClientSettings>
        </telerik:RadGrid>
    </fieldset>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEntry" runat="server">
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table style="width: 100%;">
                    <tr>
                        <td class="label">No</td>
                        <td>
                            <telerik:RadTextBox ID="txtSeqNo" runat="server" Width="50px" Enabled="False" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Date Time</td>
                        <td>
                            <telerik:RadDateTimePicker ID="txtEducationDateTime" runat="server" Width="160px" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Educator</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboEducationByUserID" Width="100%" EmptyMessage="Select a Educator" Enabled="False"
                                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="True" OnSelectedIndexChanged="cboEducationByUserID_OnSelectedIndexChanged">
                                <WebServiceSettings Method="Users" Path="~/WebService/ComboBoxDataService.asmx" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Type</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEducationType" runat="server" Width="100px" Enabled="False" />
                            &nbsp;&nbsp;Ref No:&nbsp;<telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="200px" Enabled="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Problem</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSREducationProblem" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Method</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSREducationMethod" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Other Method</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtMethodOther" Width="100%" Resize="Vertical">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Recipient</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtRecipientName" Width="100%">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Relationship</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRPatientEducationRecipient" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Evaluation</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRPatientEducationEvaluation" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Other Evaluation</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtPatientEducationEvaluationOth" Width="100%" Resize="Vertical">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Goal</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRPatientEducationGoal" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Other Goal</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtPatientEducationGoalOth" Width="100%" Resize="Vertical">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Duration (Minute)</td>
                        <td class="entry">
                            <telerik:RadNumericTextBox runat="server" ID="txtDuration" NumberFormat-DecimalDigits="0" Width="100px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Verificator</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtVerificator" Width="100%">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>

                <%--SIGN--%>
                <div class="RowSign">
                    <div class="ColumnSign">
                        <fieldset style="width: 128px">
                            <legend>Family/Patient Signature</legend>
                            <telerik:RadBinaryImage ID="fmImage" runat="server"
                                Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                            <br />
                            <asp:Button runat="server" ID="btnFmSign" Text="Sign" Width="300px" OnClientClick="javascript:editSignature1();return false;" />
                            <div>
                                <asp:HiddenField runat="server" ID="hdnImage1" />
                            </div>
                        </fieldset>
                    </div>
                    <div class="ColumnSign">
                        <fieldset style="width: 128px">
                            <legend>Educator Signature</legend>
                            <telerik:RadBinaryImage ID="psImage" runat="server"
                                Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                            <br />
                            <asp:Button runat="server" ID="btnPsSign" Text="Sign" Width="300px" OnClientClick="javascript:editSignature2();return false;" />
                            <div>
                                <asp:HiddenField runat="server" ID="hdnImage2" />
                            </div>
                        </fieldset>
                    </div>
                </div>

            </td>
            <td style="vertical-align: top;">
                <fieldset>
                    <legend>Education</legend>

                    <telerik:RadGrid ID="grdPatientEducation" Width="100%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                        AllowMultiRowSelection="True"
                        OnNeedDataSource="grdPatientEducation_NeedDataSource" OnItemDataBound="grdPatientEducation_ItemDataBound">
                        <MasterTableView DataKeyNames="ItemID" ShowHeader="true" ShowHeadersWhenNoRecords="false" Width="100%">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="" UniqueName="IsSelectedEdit" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsSelected" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText="" HeaderStyle-Width="30px" Display="False" />
                                <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="" HeaderStyle-Width="200px" />
                                <telerik:GridBoundColumn DataField="EducationNotes" UniqueName="EducationNotes" HeaderText="Notes" />
                                <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="NotesEdit">
                                    <ItemTemplate>
                                        <telerik:RadTextBox
                                            ID="txtNotes" runat="server"
                                            Width="100%">
                                        </telerik:RadTextBox>

                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                            <Resizing AllowColumnResize="False" />
                            <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
                        </ClientSettings>
                    </telerik:RadGrid>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
</asp:Content>
