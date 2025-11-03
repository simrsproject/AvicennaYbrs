<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="RasprajaEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.RasprajaEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Telerik.Web.UI.Skins" %>
<%@ Import Namespace="Temiang.Avicenna.Module.RADT.Emr" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproFlowChart.ascx" TagPrefix="uc1" TagName="RasproFlowChart" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproHeader.ascx" TagPrefix="uc1" TagName="RasproHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <style>
            .AutoHeightGridClass .rgDataDiv {
                height: auto !important;
            }
        </style>
        <script type="text/javascript">
            function onOkClick() {
                // Tampilkan tandatangan
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html';
                var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }

            function winImage_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById('<%=hdnImage.ClientID %>').value = arg.image;

                    // PostBack
                    __doPostBack("<%= btnOk.UniqueID %>", 'save');
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="optAbNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAntibioticItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="optAbYes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAntibioticItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="optComorbidNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cblComorbid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="optComorbidYes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cblComorbid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <asp:HiddenField runat="server" ID="hdnRegistrationNo" />

    <asp:HiddenField runat="server" ID="hdnImage" />
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Move, Close,Maximize,Resize"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
        ID="winImage" />

    <fieldset style="width: 1000px">
        <fieldset>
            <asp:Label runat="server" ID="lblRasproNote" Text="" Font-Size="Medium"></asp:Label>
        </fieldset>
        <table width="100%">
            <tr>
                <td style="width: 50%">
                    <table width="100%">
                        <tr>
                            <td class="label">Registration No
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" Enabled="False" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Medical No
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" Enabled="False" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Name
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" Enabled="False" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Birth Date
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="160px" DatePopupButton-Visible="False" Enabled="False" MinDate="01/01/1900" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>

                    </table>
                </td>
                <td style="width: 50%">
                    <table width="100%">
                        <tr>
                            <td class="label">Date and Time
                            </td>
                            <td class="entry">
                                <telerik:RadDateTimePicker ID="txtRasproDateTime" runat="server" Width="160px" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">DPJP
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" Enabled="False" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Service Unit
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" Enabled="False" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset style="width: 1000px">
        <legend>I. INDIKASI PENGGUNAAN ANTIBIOTIK</legend>
        <table width="600px">
            <tr>
                <td style="width: 120px;">
                    <asp:RadioButton runat="server" ID="optAbYes" GroupName="01" Text="Ada, sebutkan:" OnCheckedChanged="optAbNo_CheckedChanged" AutoPostBack="true" /></td>
                <td>
                    <telerik:RadTextBox runat="server" ID="txtAbIndication" Width="100%"></telerik:RadTextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="optAbNo" runat="server" GroupName="01" Text="Tidak Ada" OnCheckedChanged="optAbNo_CheckedChanged" AutoPostBack="true" /></td>
                <td></td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="width: 1000px">
        <legend>II. BILA TERJADI INDIKASI PENGGUNAAN ANTIBIOTIK</legend>
        <table width="600px">
            <tr>
                <td style="width: 120px;">Diagnosa Penyebab Infeksi</td>
                <td>
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
            <tr>
                <td>Gejala infeksi saat ini</td>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 120px;">
                                <asp:RadioButton runat="server" ID="optSymptomYes" GroupName="02" Text="Positif, sebutkan:" /></td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtInfectionSymptom" Width="100%"></telerik:RadTextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="optSymptomNo" runat="server" GroupName="02" Text="Negatif" /></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="width: 1000px">
        <legend>III. KOMORBID</legend>
        <table width="600px">
            <tr>
                <td style="width: 90px;">
                    <asp:RadioButton runat="server" ID="optComorbidNo" GroupName="03" Text="Tidak Ada" AutoPostBack="true" OnCheckedChanged="optComorbidNo_CheckedChanged" /></td>
                <td></td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <asp:RadioButton runat="server" ID="optComorbidYes" GroupName="03" Text="Ada" AutoPostBack="true" OnCheckedChanged="optComorbidNo_CheckedChanged" /></td>
                <td>
                    <table width="100%">
                        <tr>
                            <td>
                                <telerik:RadCheckBoxList runat="server" ID="cblComorbid" AutoPostBack="false" Enabled="false">
                                    <Items>
                                        <telerik:ButtonListItem Text="Diabetes Melitus" Value="DBMS"></telerik:ButtonListItem>
                                        <telerik:ButtonListItem Text="Imobilisasi" Value="IMOB"></telerik:ButtonListItem>
                                        <telerik:ButtonListItem Text="Retensi Sputum" Value="RTSP"></telerik:ButtonListItem>
                                        <telerik:ButtonListItem Text="Keganasan" Value="KGNS"></telerik:ButtonListItem>
                                        <telerik:ButtonListItem Text="Febrile Netropenia" Value="FBNT"></telerik:ButtonListItem>
                                        <telerik:ButtonListItem Text="Pengguanaan Instrumentasi" Value="INST"></telerik:ButtonListItem>
                                        <telerik:ButtonListItem Text="HIV / AIDS" Value="AIDS"></telerik:ButtonListItem>
                                        <telerik:ButtonListItem Text="Autoimune" Value="AIMN"></telerik:ButtonListItem>
                                    </Items>
                                </telerik:RadCheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td>Lainnya<telerik:RadTextBox runat="server" ID="txtOtherComorbid" Width="100%"></telerik:RadTextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="width: 1000px">
        <legend>IV. ANTIBIOTIKA YANG DIGUNAKAN</legend>
        <telerik:RadGrid ID="grdAntibioticItem" Width="100%" runat="server" OnNeedDataSource="grdAntibioticItem_NeedDataSource" OnItemDataBound="grdAntibioticItem_ItemDataBound"
            AutoGenerateColumns="False" GridLines="None" AllowMultiRowSelection="True"
            ShowHeader="true" AllowMultiRowEdit="false">
            <MasterTableView DataKeyNames="RasproSeqNo,ItemID">
                <CommandItemStyle Height="29px" />
                <Columns>
                    <telerik:GridClientSelectColumn UniqueName="Select" HeaderStyle-Width="30px">
                    </telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Product" UniqueName="ItemName"
                        HeaderStyle-Width="250px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="ZatActiveName" HeaderText="Zat Active" UniqueName="ZatActiveName"
                        HeaderStyle-Width="180px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridTemplateColumn HeaderText="Consume Method" UniqueName="SRConsumeMethodName" HeaderStyle-Width="120px"
                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%# string.Format("{0} @{1} {2}", DataBinder.Eval(Container.DataItem, "SRConsumeMethodName"),  DataBinder.Eval(Container.DataItem, "ConsumeQty"),   DataBinder.Eval(Container.DataItem, "SRConsumeUnit")) %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="ConsumeDayNo" HeaderText="Day" UniqueName="ConsumeDayNo"
                        HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="false">
                <Resizing AllowColumnResize="false" />
                <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True"></Selecting>

            </ClientSettings>
        </telerik:RadGrid>
        <br />
        Alasan penggunaan antibiotik diluar PPAB / jangka waktu diluar ketentuan: </br>
                            <telerik:RadTextBox runat="server" ID="txtRasprajaReason" Width="100%"></telerik:RadTextBox>

    </fieldset>
    <fieldset style="width: 1000px">
        <legend>IV. KONSULTASI DENGAN TEAM PPRA</legend>
        <table width="600px">
            <tr>
                <td>
                    <asp:RadioButton runat="server" ID="optPpraConsultYes" GroupName="05" Text="Ada" /></td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="optPpraConsultNo" runat="server" GroupName="05" Text="Tidak Ada, alasan:" /></td>

            </tr>
            <tr>
                <td>
                    <telerik:RadTextBox runat="server" ID="txtNotPpraConsultReason" Width="100%"></telerik:RadTextBox>
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:Panel runat="server" ID="panMenu" class="footer">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnOk" runat="server" Text="Ok" Width="70" OnClientClick="onOkClick();return false;" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70" OnClientClick="Close();return false;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
