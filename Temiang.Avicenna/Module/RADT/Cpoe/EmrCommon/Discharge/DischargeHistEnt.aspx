<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogHistEntry.Master" AutoEventWireup="true"
    CodeBehind="DischargeHistEnt.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.DischargeHistEnt" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphEntry" runat="server">
    <telerik:RadCodeBlock runat="server" ID="cb">
        <script type="text/javascript" language="javascript">
            function openParSign() {
                <%=IsModeViewHist? "return":string.Empty%>
                var imgId = '<%=imgParSign.ClientID %>';
                var txtId = '<%=hdnParSign.ClientID %>';
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=edt&imgId=' + imgId + '&txtId=' + txtId;
                var oWnd = $find("<%= winSign.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }


            function winSign_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById(arg.txtId).value = arg.image;
                    var img = document.getElementById(arg.imgId);
                    img.setAttribute('src', "data:image/Png;base64," + arg.image);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winSign_ClientClose"
        ID="winSign" />
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">Discharge Date / Time
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDischargeDate" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtDischargeTime" runat="server" Width="93px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvDischargeDate" runat="server" ErrorMessage="Discharge Date required."
                                ValidationGroup="entry" ControlToValidate="txtDischargeDate" SetFocusOnError="True">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>&nbsp;
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Discharge Method
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRDischargeMethod" runat="server" Width="304px" />
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvSRDischargeMethod" runat="server" ErrorMessage="Discharge Method required."
                                ValidationGroup="entry" ControlToValidate="cboSRDischargeMethod" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Discharge Condition
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRDischargeCondition" runat="server" Width="304px" />
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvSRDischargeCondition" runat="server" ErrorMessage="Discharge Condition required."
                                ValidationGroup="entry" ControlToValidate="cboSRDischargeCondition" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Discharge Medical Notes
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtDischargeMedicalNotes" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Discharge Notes
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtDischargeNotes" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">Treating Physician
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboTreatingPhysician" runat="server" Width="304px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">(or type here)
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtTreatingPhysicianName" runat="server" Width="300px" MaxLength="250" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Inpatient Indication
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRUnitIntended" runat="server" Width="304px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td colspan="3">
                            <fieldset style="width: 128px">
                                <legend>Physician Signature</legend>
                                <a onclick="javascript:openParSign();return false;">
                                    <telerik:RadBinaryImage ID="imgParSign" runat="server"
                                        Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                                </a>
                                <br />
                                <asp:Label runat="server" ID="lblSignDate" Text="" Width="294px" BorderStyle="Solid" Style="text-align: center; border-color: gray;"></asp:Label>
                                <br />
                                <asp:Button runat="server" ID="btnParSign" Text="Sign" Width="296px" OnClientClick="javascript:openParSign();return false;" />
                                <div>
                                    <asp:HiddenField runat="server" ID="hdnParSign" />
                                </div>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphList" runat="server">
    <div class="divcaption" style="width: 100%;">Discharge History</div>
    <telerik:RadGrid ID="grdDischargeHist" runat="server" OnNeedDataSource="grdDischargeHist_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" Height="560px"
        ExpandCollapseColumn-Display="true">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo" GroupLoadMode="Client">
            <NestedViewTemplate>
                <table width="100%">
                    <tr>
                        <td style="width: 30%; vertical-align: top;">
                            <fieldset>
                                <legend>Diagnosis</legend>
                                <%#DataBinder.Eval(Container.DataItem, "Diagnosis") %><br />
                                <%#DataBinder.Eval(Container.DataItem, "ICD10")%>
                            </fieldset>
                        </td>
                        <td style="vertical-align: top;">
                            <fieldset>
                                <legend>Therapy</legend>
                                <%#DataBinder.Eval(Container.DataItem, "Therapy")%>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </NestedViewTemplate>

            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="RegistrationDateTime" HeaderText="Date"
                    UniqueName="RegistrationDateTime" SortExpression="RegistrationDateTime" DataType="System.DateTime"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="105px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="BedID" HeaderText="Bed No"
                    UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DischargeDate" HeaderText="Discharge"
                    UniqueName="DischargeDate" SortExpression="DischargeDate" DataType="System.DateTime"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="105px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DischargeMethod" HeaderText="Discharge Method"
                    UniqueName="DischargeMethod" SortExpression="DischargeMethod" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DischargeCondition" HeaderText="Condition" UniqueName="DischargeCondition"
                    SortExpression="DischargeCondition" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
