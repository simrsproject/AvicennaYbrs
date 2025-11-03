<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="UpdateMrnRegistrationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.UpdateMrnRegistrationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboPatientID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtSalutation2" />
                    <telerik:AjaxUpdatedControl ControlID="txtPatientName2" />
                    <telerik:AjaxUpdatedControl ControlID="txtGender2" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlaceDOB2" />
                    <telerik:AjaxUpdatedControl ControlID="txtAddress2" />
                    
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <table width="100%" cellpadding="1" cellspacing="5">
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label3" runat="server" Text="UPDATE MRN FOR" Font-Bold="True" Font-Size="9"></asp:Label>
                    </legend>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" ReadOnly="true" />
                                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                                    Text=""></asp:Label>&nbsp; </a>
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                                    </td>
                                                    <td style="width: 3px">
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="245px" ReadOnly="true" />
                                                    </td>
                                                    <td style="width: 3px">
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" TextMode="MultiLine"
                                                ReadOnly="true" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date/Time"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDateTimePicker ID="txtRegistrationDateTime" runat="server" AutoPostBackControl="None"
                                                Enabled="False" Width="170px">
                                                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd-MMM-yyyy HH:mm" DateFormat="dd-MMM-yyyy HH:mm">
                                                </DateInput>
                                                <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm">
                                                </TimeView>
                                            </telerik:RadDateTimePicker>
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtServiceUnit" runat="server" Width="300px" MaxLength="20"
                                                ReadOnly="true" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRoomBed" runat="server" Text="Room / Bed No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtRoomBed" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblParamedic" runat="server" Text="Physician"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtParamedic" runat="server" Width="300px" MaxLength="20"
                                                ReadOnly="true" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5">
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label1" runat="server" Text="UPDATE TO" Font-Bold="True" Font-Size="9"></asp:Label></legend>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPatienID" runat="server" Text="Medical No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPatientID_ItemsRequested"
                                                OnItemDataBound="cboPatientID_ItemDataBound" AutoPostBack="true" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                                    <br />
                                                    <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 5 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 20px">
                                            <asp:RequiredFieldValidator ID="rfvPatientID" runat="server" ErrorMessage="Medical No required."
                                                ValidationGroup="entry" ControlToValidate="cboPatientID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPatientName2" runat="server" Text="Patient Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtSalutation2" runat="server" Width="28px" ReadOnly="true" />
                                                    </td>
                                                    <td style="width: 3px">
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtPatientName2" runat="server" Width="245px" ReadOnly="true" />
                                                    </td>
                                                    <td style="width: 3px">
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtGender2" runat="server" Width="22px" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPlaceDOB2" runat="server" Text="City / Date Of Birth"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPlaceDOB2" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAddress2" runat="server" Text="Address"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtAddress2" runat="server" Width="300px" TextMode="MultiLine"
                                                ReadOnly="true" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%" valign="top">
                                <table width="100%">
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
