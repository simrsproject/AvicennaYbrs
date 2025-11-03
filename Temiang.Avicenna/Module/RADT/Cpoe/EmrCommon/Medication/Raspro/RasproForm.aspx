<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="RasproForm.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.RasproForm" %>

<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproHeader.ascx" TagPrefix="uc1" TagName="RasproHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        window.onbeforeunload = null; //Override master onbeforeunload

        $.download = function (btnID, url, data, method) {
            //url and data options required
            if (url && data) {

                var btn = document.getElementById(btnID);
                btn.disabled = true;
                btn.value = 'Downloading';

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

    </script>
    <asp:HiddenField runat="server" ID="hdnRasproLineID" />
    <asp:HiddenField runat="server" ID="hdnRasproLineSeqNo" />
    <uc1:RasproHeader runat="server" ID="rasproHeader" />

    <br />
    <fieldset>
        <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
        <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="entry"
            ErrorMessage="" OnServerValidate="customValidator_ServerValidate" Display="None"></asp:CustomValidator>
        <table width="100%">
            <tr>
                <td class="label">Advise By
                </td>
                <td class="entry">
                    <telerik:RadComboBox runat="server" ID="cboAdviseByParamedicID" Width="100%" EmptyMessage="Select a Paramedic"
                        EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                        <WebServiceSettings Method="Paramedics" Path="~/WebService/ComboBoxDataService.asmx" />
                        <ClientItemTemplate>
                            <div>
                                <ul class="details">
                                    <li class="bold">
                                        <span>#= Text # </span>
                                    </li>
                                    <li class="smaller">
                                        <span>#= Attributes.SpecialtyName # </span>
                                    </li>
                                </ul>
                            </div>
                        </ClientItemTemplate>
                    </telerik:RadComboBox>
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvAdviseBy" runat="server" ErrorMessage="Advise By can't empty"
                        ValidationGroup="entry" ControlToValidate="cboAdviseByParamedicID" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>

                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="lblSpecification" Font-Size="12px"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadDropDownTree runat="server" ID="cboAbRestrictionID" Width="100%"
                        EnableFiltering="true" DefaultMessage="Select" DefaultValue="0" DataValueField="AbRestrictionID"
                        DataTextField="AbRestrictionName" DataFieldID="AbRestrictionID" DataFieldParentID="ParentID">
                        <DropDownSettings Height="400px" CloseDropDownOnSelection="true" />
                        <FilterSettings Highlight="Matches" Filter="Contains" EmptyMessage="Type here to filter" />
                    </telerik:RadDropDownTree>
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage=""
                        ValidationGroup="entry" ControlToValidate="cboAbRestrictionID" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Other Bacterial Infection Description</td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtOtherInfection" runat="server" Width="100%" MaxLength="500" />
                </td>
                <td width="20"><asp:Image ID="imgRfvOtherInfection" runat="server" SkinID="rfvImage" Visible="false" /></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Work Diagnose and Other</td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtDiagnose" runat="server" TextMode="MultiLine" Width="100%" Height="300px" MaxLength="2000" />
                </td>
                <td width="20">
                    <asp:RequiredFieldValidator ID="rfv02" runat="server" ErrorMessage="Diagnose can't empty"
                        ValidationGroup="entry" ControlToValidate="txtDiagnose" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator></td>
                <td></td>
            </tr>
        </table>
    </fieldset>
    <asp:Panel runat="server" ID="panMenu" class="footer">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnPpabPdf" runat="server" Text="Download PPAB File" Width="150" OnClientClick="$.download(this.id,'DownloadFile.aspx','id=ppab'); return false;" />&nbsp;
                    <asp:Button ID="btnNext" runat="server" Text="Next >>" Width="70" OnClick="btnNext_Click" ValidationGroup="entry" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70" OnClientClick="Close();return false;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
