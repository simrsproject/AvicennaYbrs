<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogHistEntry.Master" AutoEventWireup="true"
    CodeBehind="PatientDocumentHistEnt.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.PatientDocumentHistEnt" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphList" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" src="<%= Helper.UrlRoot() %>/JavaScript/jquery.js"></script>
        <script type="text/javascript" language="javascript">
            $.download = function (url, data, method) {
                //url and data options required
                if (url && data) {
                    //data can be string of parameters or array/object
                    data = typeof data == 'string' ? data : $.param(data);
                    //split params into form inputs
                    var inputs = '';
                    $.each(data.split('&'), function () {
                        var pair = this.split('=');
                        inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
                    });
                    //send request
                    $('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>').appendTo('body').submit().remove();
                };
            };
            function ShowFile(id, fileName) {
                if (fileName.toLowerCase().includes(".dcm") == true) // DICOM file gunakan external app
                {
                    $.download("PatientDocumentDownload.aspx", "id=" + id);
                }
                else {
                    var url = "PatientDocumentImageZoomView.aspx?id=" + id+"&patid=<%= PatientID %>";
                    if (fileName.toLowerCase().includes(".pdf") == true)
                        url = "<%= Helper.UrlRoot() %>/Module/Reports/PdfUrlViewer.aspx?mode=patdoc&id=" + id;

                    openWinMaximize(url);
                }
            }
            function openWinMaximize(url) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.maximize();
            }

            function batchUpload() {
                var url = "PatientDocumentBatchUpload.aspx";
                var oWnd = $find("<%= winBatchUpload.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.maximize();
            }
            function winBatchUpload_ClientClose(oWnd, args) {
                var masterTable = $find("<%= grdDocument.ClientID %>").get_masterTableView();
                masterTable.rebind();
            }
            function applyGridHeightMax() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                // set height to the whole RadGrid control
                var grid = $find("<%= grdDocument.ClientID %>");
                grid.get_element().style.height = height - 40 + "px";
                grid.repaint();
            }
            window.onload = function () {
                applyGridHeightMax();
            }
            window.onresize = function () {
                applyGridHeightMax();
            }

            // After postback
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (s, e) {
                applyGridHeightMax();
            });
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadWindow ID="winDialog" Width="600px" Height="600px" runat="server" Title=" "
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" VisibleStatusbar="False">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winBatchUpload" Width="600px" Height="600px" runat="server" OnClientClose="winBatchUpload_ClientClose"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" VisibleStatusbar="False">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdDocument">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocument" />
                    <telerik:AjaxUpdatedControl ControlID="cphEntry" />
                    <telerik:AjaxUpdatedControl ControlID="fw_tbarData" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="fw_tbarData">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cphEntry" />
                    <telerik:AjaxUpdatedControl ControlID="fw_tbarData" />
                    <telerik:AjaxUpdatedControl ControlID="grdDocument" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkIsCurrentReg">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocument" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocument" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <iframe id="iframe" style="display: none;"></iframe>
    <table style="width: 100%;">
        <tr>
            <td style="width: 150px;">Patient Document List</td>
            <td style="width: 150px;">
                <telerik:RadCheckBox runat="server" Width="150px" ID="chkIsCurrentReg" Text="Current Registration" AutoPostBack="True" OnCheckedChanged="OnCheckedChanged" Checked="false"></telerik:RadCheckBox>
            </td>
            <td style="width: 60px;">Search</td>
            <td style="width: 300px;">
                <telerik:RadTextBox runat="server" ID="txtSearchDocumentName" Width="300px"></telerik:RadTextBox></td>
            <td style="text-align: left">
                <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnFilter_Click" ToolTip="Search" />
            </td>
            <td></td>
        </tr>
    </table>
    <telerik:RadGrid runat="server" ID="grdDocument" OnNeedDataSource="grdDocument_NeedDataSource"
        OnItemCommand="grdDocument_ItemCommand"
        Height="530px"
        AllowSorting="true">
        <MasterTableView ShowHeader="true" AutoGenerateColumns="False" AllowPaging="false"
            DataKeyNames="PatientDocumentID">
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" CommandName="View" ToolTip='View'>
                            <img src="../../../../../Images/Toolbar/views16.png" border="0" alt=""/>
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="File" HeaderText="File">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FileAttachName").ToString() == string.Empty ? string.Empty : string.Format("<a href=\"#\" onclick=\"$.download('PatientDocumentDownload.aspx','id={0}'); return false;\"><img src=\"../../../../../Images/Toolbar/download16.png\" border=\"0\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "PatientDocumentID"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="colSmallImage" HeaderText="" HeaderStyle-Width="130px">
                    <ItemTemplate>
                        <div style="width: 130px; text-align: center;">
                            <asp:LinkButton ID="lbtnDocumentImage" runat="server" ToolTip="View"
                                OnClientClick='<%#string.Format("javascript:ShowFile(\"{0}\",\"{1}\");return false;",DataBinder.Eval(Container.DataItem, "PatientDocumentID"), Eval("FileAttachName"))%>'>
                                <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                    Width="125px" Height="125px" ResizeMode="Fit" DataValue='<%# Eval("SmallImage") == DBNull.Value? new System.Byte[0]: Eval("SmallImage") %>'></telerik:RadBinaryImage>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="DocumentName" HeaderText="Document Name"
                    UniqueName="DocumentName" SortExpression="DocumentName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="DocumentDate" HeaderText="DocumentDate"
                    UniqueName="Reference" SortExpression="DocumentDate" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                    SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEntry" runat="server">
    <asp:HiddenField runat="server" ID="hdnPdId" />
    <fieldset>
        <legend>Single Upload</legend>
        <table width="100%">
            <tr>
                <td class="label">
                    <asp:Label ID="Label3" runat="server" Text="Document Date"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadDatePicker ID="txtDocumentDate" runat="server" Width="100px" />
                </td>
                <td width="20px"></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblDocumentName" runat="server" Text="Document Name"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtDocumentName" runat="server" Width="100%" />
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvDocumentName" runat="server" ErrorMessage="Document Name required."
                        ValidationGroup="entry" ControlToValidate="txtDocumentName" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label1" runat="server" Text="Notes"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" TextMode="MultiLine"
                        Height="200px" />
                </td>
                <td width="20px"></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label2" runat="server" Text="File Document"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtFileAttachName" runat="server" Width="100%" ReadOnly="true" />
                </td>
                <td width="20px"></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label4" runat="server" Text="Upload File"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadAsyncUpload ID="uplFileTemplate" runat="server" ControlObjectsVisibility="None"
                        Width="100%" InitialFileInputsCount="1" MaxFileInputsCount="1" AllowedFileExtensions=".jpeg,.jpg,.png,.pdf,.dcm">
                    </telerik:RadAsyncUpload>
                </td>
                <td width="20px"></td>
                <td></td>
            </tr>
        </table>
    </fieldset>
    <br />
    <br />
    <fieldset>
        <legend>Batch Upload</legend>
        <telerik:RadButton runat="server" ID="btnBathUpload" Text="Click here for Batch Upload" OnClientClicking="batchUpload" AutoPostBack="False"></telerik:RadButton>
    </fieldset>
</asp:Content>
