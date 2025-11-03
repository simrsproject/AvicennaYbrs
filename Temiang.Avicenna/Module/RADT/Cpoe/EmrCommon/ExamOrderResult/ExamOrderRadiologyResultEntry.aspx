<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="ExamOrderRadiologyResultEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ExamOrderRadiologyResultEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function UploadImage(no) {
            var url = "ExamOrderRadiologyImageUpload.aspx?trno=<%= TransactionNo %>&seqno=<%= SequenceNo %>&imgno=" + no + '&ccm=rebind&cet=<%=lvItemDocumentImage.ClientID %>';
            var oWnd = $find("<%=winUploadImage.ClientID %>");
            oWnd.setUrl(url);
            oWnd.show();
            oWnd.setSize(800, 700);
            oWnd.center();

            // Cek position
            var pos = oWnd.getWindowBounds();
            if (pos.y<0)
                oWnd.moveTo(pos.x, 0);
        }
        function ZoomViewImage(no) {
            var url = "ExamOrderImageZoomView.aspx?trno=<%= TransactionNo %>&seqno=<%= SequenceNo %>&imgno=" + no;
            var wnd = $find("<%=winUploadImage.ClientID %>");
            wnd.setUrl(url);
            wnd.show();
            wnd.maximize();
        }

        function winUploadImage_ClientClose(oWnd, args) {
            var arg = args.get_argument();
            if (arg != null) {
                if (arg.callbackMethod === 'rebind') {
                    var ctl = $find(arg.eventTarget);
                    if (typeof ctl.rebind == 'function') {
                        ctl.rebind();
                    }
                }
            }
        }

    </script>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 500px; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="TransactionNo"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="197px" MaxLength="20"
                                ReadOnly="true" />
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="105px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClinicalInfo" runat="server" Text="Clinical Information"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClinicalInfo" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 500px; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">Test Code
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Test Name
                        </td>
                        <td class="entry">
                            <asp:Label ID="lblItemName" runat="server" Font-Bold="True" font-Size="12" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTestResultTemplateID" runat="server" Text="Result Template"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboTestResultTemplateID" Width="304px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboTestResultTemplateID_ItemDataBound"
                                OnItemsRequested="cboTestResultTemplateID_ItemsRequested" OnSelectedIndexChanged="cboTestResultTemplateID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "TestResultTemplateName")%>
                                    &nbsp;(<b><%# DataBinder.Eval(Container.DataItem, "TestResultTemplateID")%></b>)
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Physician Sender"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboPhysicianSender" Width="304px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="false" AutoPostBack="false" AllowCustomText="true" OnItemDataBound="cboPhysicianSender_ItemDataBound"
                                OnItemsRequested="cboPhysicianSender_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEntryDateTime" runat="server" Text="Entry Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtEntryDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtEntryTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="true">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEntryDate" runat="server" ErrorMessage="Entry Date required."
                                ValidationGroup="entry" ControlToValidate="txtEntryDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <fieldset id="FieldSet1" style="width: 200px; min-height: 200px;">
                                <legend>Photo</legend>
                                <asp:Image runat="server" ID="imgPatientPhoto" Width="200px" Height="200px" />
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td width="25%" class="labelcaption">
                            <asp:Label runat="server" ID="lblS" Text="(S) Subjective" />
                        </td>
                        <td width="25%" class="labelcaption">
                            <asp:Label runat="server" ID="lblO" Text="(O) Objective" />
                        </td>
                        <td width="25%" class="labelcaption">
                            <asp:Label runat="server" ID="lblA" Text="(A) Assessment" />
                        </td>
                        <td width="25%" class="labelcaption">
                            <asp:Label runat="server" ID="lblP" Text="(P) Planning" />
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <telerik:RadTextBox ID="txtS" runat="server" Width="98%" ReadOnly="true" Height="73px"
                                TextMode="MultiLine" />
                        </td>
                        <td width="25%">
                            <telerik:RadTextBox ID="txtO" runat="server" Width="98%" ReadOnly="true" Height="73px"
                                TextMode="MultiLine" />
                        </td>
                        <td width="25%">
                            <telerik:RadTextBox ID="txtA" runat="server" Width="98%" ReadOnly="true" Height="73px"
                                TextMode="MultiLine" />
                        </td>
                        <td width="25%">
                            <telerik:RadTextBox ID="txtP" runat="server" Width="98%" ReadOnly="true" Height="73px"
                                TextMode="MultiLine" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Result in Native Language" PageViewID="pgNative" Selected="True" />
            <telerik:RadTab runat="server" Text="Result in Foreign Language" PageViewID="pgOther" />
            <telerik:RadTab runat="server" Text="Document Image" PageViewID="pgDocumentImage" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgNative" runat="server" Selected="true">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblResult" runat="server" Text="Result*"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestResult" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblSummary" runat="server" Text="Summary"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestSummary" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblSuggest" runat="server" Text="Suggest"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestSuggest" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgOther" runat="server">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Result"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestResultOtherLang" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="Label4" runat="server" Text="Summary"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestSummaryOtherLang" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="Label5" runat="server" Text="Suggest"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadEditor ID="txtTestSuggestOtherLang" runat="server" Width="1000px" Height="500px" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgDocumentImage" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 250px;">
                        <asp:HiddenField runat="server" ID="hdnImageNoForEdit" />

                        <telerik:RadWindow ID="winUploadImage" Width="900px" Height="600px" runat="server" OnClientClose="winUploadImage_ClientClose"
                            ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="False" ShowOnTopWhenMaximized="True" VisibleStatusbar="False">
                        </telerik:RadWindow>
                        <asp:LinkButton ID="lbtnDocumentImageAdd" runat="server" ToolTip="Add Image"
                            OnClientClick='javascript:UploadImage("");return false;'>
                            <img src="../../../../../Images/Toolbar/insert16.png"/>
                        </asp:LinkButton>
                        <telerik:RadListView ID="lvItemDocumentImage" runat="server" RenderMode="Lightweight"
                            ItemPlaceholderID="ImageContainer" OnNeedDataSource="lvItemDocumentImage_NeedDataSource">

                            <LayoutTemplate>
                                <fieldset style="height: 150px; overflow: auto;">
                                    <legend>Document Image</legend>
                                    <table>
                                        <tr>
                                            <asp:PlaceHolder ID="ImageContainer" runat="server"></asp:PlaceHolder>
                                        </tr>
                                    </table>
                                </fieldset>

                            </LayoutTemplate>
                            <ItemTemplate>
                                <td style="height: 125px; width: 225px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lbtnDocumentImage" runat="server" ToolTip="Zoom"
                                                    OnClientClick='<%#string.Format("javascript:ZoomViewImage({0});return false;",DataBinder.Eval(Container.DataItem, "ImageNo"))%>'>
                                                    <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                                        Width="125px" Height="125px" ResizeMode="Fit" DataValue='<%# Eval("DocumentImage") == DBNull.Value? new System.Byte[0]: Eval("DocumentImage") %>'></telerik:RadBinaryImage>
                                                </asp:LinkButton>
                                            </td>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td colspan="2"><%#DataBinder.Eval(Container.DataItem, "DocumentName")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50px;">Add:</td>
                                                        <td><%#string.Format("{0}",Eval("CreatedDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("CreatedDateTime")).ToString(AppConstant.DisplayFormat.Date))%></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">

                                                            <asp:LinkButton ID="lbtnDocumentImageEdit" runat="server" ToolTip="Edit"
                                                                OnClientClick='<%#string.Format("javascript:UploadImage({0});return false;",DataBinder.Eval(Container.DataItem, "ImageNo"))%>'>
                                                                <img src="../../../../../Images/Toolbar/edit16.png"/>
                                                            </asp:LinkButton>

                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:LinkButton ID="lbtnDocumentImageDelete" runat="server" ToolTip="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ImageNo")%>'
                                                                OnClientClick='return confirm("Delete this document image?")' OnClick="lbtnDocumentImageDelete_OnClick">
                                                                                                                                                                          <img src="../../../../../Images/Toolbar/delete16.png"/>
                                                            </asp:LinkButton>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                    </table>
                                </td>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </td>
                    <td></td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

</asp:Content>
