<%@ Page Title="Booking Schedule Detail" Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master"
    AutoEventWireup="true" CodeBehind="ServiceUnitBookingOperationNotesEntry.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitBookingOperationNotesEntry" %>

<%@ Register Assembly="Temiang.Avicenna" Namespace="Temiang.Avicenna.CustomControl"
    TagPrefix="cc1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        function onWinLookUpClose(oWnd, args) {
            if (oWnd.argument) {
                var txt = $find("<%= txtOperatingNotes.ClientID %>");

                var textValue = txt.get_textBoxValue();
                if (oWnd.argument.notes !== "undefined" && oWnd.argument.notes!== "" && textValue !== "undefined" && textValue !== "") {
                    if (!confirm("Existing operation notes will replaced. Continue ?"))
                        return;
                }

                if (oWnd.argument.notes !== "") {
                    var notes = decodeURIComponent((oWnd.argument.notes + '').replace(/\+/g, '%20'));
                    txt.set_value(notes);
                }

                oWnd.argument.notes = "";
            }
        }

        function openOperatingNotesTemplate() {
            var url = 'OperatingNotesTemplateCopy.aspx?parid=<%= ParamedicID %>&ccm=submit&cet=<%=txtOperatingNotes.ClientID %>';
            var oWnd = $find("<%= winLookUp.ClientID %>");
            oWnd.setUrl(url);
            oWnd.center();
            oWnd.show();

            // Cek position
            var pos = oWnd.getWindowBounds();
            if (pos.y<0)
                oWnd.moveTo(pos.x, 0);
        }


        function openOperatingNotesTemplateNew() {
            var txt = $find("<%= txtOperatingNotes.ClientID %>");
            var textValue = txt.get_textBoxValue();
            var url = 'OperatingNotesTemplateNew.aspx?parid=<%= ParamedicID %>&notes=' +encodeURIComponent(textValue);
            var oWnd = $find("<%= winNewTemplate.ClientID %>");
            oWnd.setUrl(url);
            oWnd.center();
            oWnd.show();

            // Cek position
            var pos = oWnd.getWindowBounds();
            if (pos.y<0)
                oWnd.moveTo(pos.x, 0);
        }
    </script>

    <telerik:RadWindow ID="winLookUp" Animation="None" Width="800px" Height="450px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false" 
        Modal="true" OnClientClose="onWinLookUpClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winNewTemplate" Animation="None" Width="800px" Height="450px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <table width="100%">
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="lblPatientDetail" runat="server" Text="Patient Detail"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBookingNo" runat="server" Text="Booking No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBookingNo" runat="server" Width="100%" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="100%" ReadOnly="true" />
                        </td>
                        <td witdh="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientID" runat="server" Width="100%" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100%" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="100%" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButton runat="server" ID="rbMale" Text="Male" />
                            &nbsp;
                            <asp:RadioButton runat="server" ID="rbFemale" Text="Female" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtYear" Width="50px" ReadOnly="true" />&nbsp;y
                            &nbsp;
                            <telerik:RadTextBox runat="server" ID="txtMonth" Width="50px" ReadOnly="true" />&nbsp;m
                            &nbsp;
                            <telerik:RadTextBox runat="server" ID="txtDay" Width="50px" ReadOnly="true" />&nbsp;d
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" ReadOnly="true"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysician" runat="server" Text="Physician #1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhysician1" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <fieldset style="width: 98%">
        <legend>Operating Note </legend>
        <div>
            <%= ScriptCopyTemplate() %>&nbsp;<%= ScriptNewTemplate() %>
        </div>
        <telerik:RadTextBox ID="txtOperatingNotes" runat="server" Width="100%" Height="285px" ReadOnly="false"
            TextMode="MultiLine" />
    </fieldset>
</asp:Content>
