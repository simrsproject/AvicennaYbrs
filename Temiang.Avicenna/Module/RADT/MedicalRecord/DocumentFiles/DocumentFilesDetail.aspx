<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="DocumentFilesDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.DocumentFilesDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="140px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Upload Document Template"
        OnClientClose="onWinUploadClientClose" ID="winUpload">
    </telerik:RadWindow>
    <table width="100%">
        <tr style="display: none">
            <td class="label">
                <asp:Label ID="lblDocumentFilesID" runat="server" Text="Document Files ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtDocumentFilesID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvDocumentFilesID" runat="server" ErrorMessage="Document Files ID required."
                    ValidationGroup="entry" ControlToValidate="txtDocumentFilesID" SetFocusOnError="True"
                    Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDocumentNumber" runat="server" Text="Document No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDocumentNumber" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvDocumentNumber" runat="server" ErrorMessage="Document Number required."
                    ValidationGroup="entry" ControlToValidate="txtDocumentNumber" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDocumentName" runat="server" Text="Document Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDocumentName" runat="server" Width="300px" MaxLength="300"
                    TextMode="MultiLine" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvDocumentName" runat="server" ErrorMessage="Document Name required."
                    ValidationGroup="entry" ControlToValidate="txtDocumentName" SetFocusOnError="True"
                    Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Document Initial"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDocumentInitial" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <tr runat="server" id="trDocumentType">
            <td class="label">Document Type
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRDocumentFileType" runat="server" Width="300px" MarkFirstMatch="true" OnSelectedIndexChanged="cboSRDocumentFileType_SelectedIndexChanged" AutoPostBack="true">
                </telerik:RadComboBox>
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <tr runat="server" id="trAssessmentType" visible="false">
            <td class="label">Assessment Type
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRAssessmentType" runat="server" Width="300px" MarkFirstMatch="true" AllowCustomText="true">
                </telerik:RadComboBox>
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <tr runat="server" id="trHaisMonitoring" visible="false">
            <td class="label">Monitoring Type (Hais)
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRHaisMonitoring" runat="server" Width="300px" MarkFirstMatch="true" AllowCustomText="true">
                </telerik:RadComboBox>
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <tr id="trQuestionFormID" runat="server">
            <td class="label">Question Form (PHR)
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboQuestionFormID" runat="server" Width="300px" MarkFirstMatch="true"
                    HighlightTemplatedItems="true" EnableLoadOnDemand="true" OnItemDataBound="cboQuestionFormID_ItemDataBound"
                    OnItemsRequested="cboQuestionFormID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "QuestionFormID")%>&nbsp;-&nbsp;<%# DataBinder.Eval(Container.DataItem, "QuestionFormName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <tr runat="server" id="trProgramID">
            <td class="label">Program ID
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboProgramID" runat="server" Width="300px" MarkFirstMatch="true"
                    HighlightTemplatedItems="true" EnableLoadOnDemand="true" OnItemDataBound="cboProgramID_ItemDataBound"
                    OnItemsRequested="cboProgramID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ProgramID")%>&nbsp;-&nbsp;<%# DataBinder.Eval(Container.DataItem, "ProgramName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <asp:Panel runat="server" ID="pnlMedicalRecordDoc">
            <tr>
                <td class="label">
                    <asp:Label ID="Label1" runat="server" Text="Status"></asp:Label>
                </td>
                <td class="entry">
                    <asp:CheckBox ID="chkIsQuality" runat="server" Text="Quality" />
                    <asp:CheckBox ID="chkIsLegible" runat="server" Text="Legible" />
                    <asp:CheckBox ID="chkIsSign" runat="server" Text="Signature" />
                </td>
                <td width="20px"></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label2" runat="server" Text="File Template"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtFileTemplateName" runat="server" Width="100%" ReadOnly="true" />
                </td>
                <td width="20px">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClientClick="javascript:openWinUpload();return false;" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label"></td>
                <td class="entry">
                    <asp:CheckBox ID="chkIsUsedForAnalysis" runat="server" Text="Used For Analysis" />
                </td>
                <td width="20px"></td>
                <td></td>
            </tr>
        </asp:Panel>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
    </table>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">
            function openWinUpload() {
                var oWnd = window.$find("<%= winUpload.ClientID %>");
                oWnd.setUrl("DocumentFilesUpload.aspx?dfid=<%=txtDocumentFilesID.Text%>");
                oWnd.show();
            }
            function onWinUploadClientClose(oWnd) {
                //Jika apply di click
                var result = oWnd.argument;
                if (result) {
                    window.$find("<%= txtFileTemplateName.ClientID %>").set_value(result);;
                }
                result = null;
            }
        </script>

    </telerik:RadScriptBlock>
</asp:Content>
