<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="RaspaturEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.RaspaturEntry" %>

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
    <asp:HiddenField runat="server" ID="hdnRegistrationNo" />

    <asp:HiddenField runat="server" ID="hdnImage" />
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Move, Close,Maximize,Resize"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
        ID="winImage" />

    <fieldset style="width: 1000px">
        <legend>
            <asp:Label runat="server" ID="lblRasproName" Text="FORMULIR RASPRO<br/>ANTIBIOTIK SESUAI KULTUR (RASPATUR)" Font-Bold="True" Font-Size="14px"></asp:Label>
        </legend>
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
                            </td>
                            <td class="entry">
                                <telerik:RadCheckBox ID="chkIsExternalCultureLabTest" Text="Culture test result from external laboratory" runat="server" AutoPostBack="false" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="width: 1000px">
        <legend>ANTIBIOTIK</legend>
        <div style="font-style: italic;">(*Antibiotik diisi dientrian resep setelah form ini diclose)</div>
    </fieldset>
    <fieldset style="width: 1000px">
        <legend>Culture Test result</legend>
        <asp:HiddenField runat="server" ID="hdnTransactionNo" />
        <telerik:RadGrid ID="grdLaboratory" runat="server" OnNeedDataSource="grdLaboratory_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" Height="400px"
            OnItemDataBound="grdLaboratory_OnItemDataBound">
            <MasterTableView DataKeyNames="TransactionNo" CommandItemDisplay="None">
                <Columns>
                    <telerik:GridDateTimeColumn DataField="TransactionDate" UniqueName="TransactionDate"
                        HeaderText="Date" HeaderStyle-Width="80px" ItemStyle-VerticalAlign="Top" />
                    <telerik:GridTemplateColumn UniqueName="OrderBy" HeaderText="Order" HeaderStyle-Width="200px">
                        <ItemTemplate>
                            Reg No:&nbsp;<%#DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                            <br />
                            Tx No:&nbsp;<%#DataBinder.Eval(Container.DataItem, "TransactionNo")%>
                            <br />
                            From:&nbsp;<b><%#DataBinder.Eval(Container.DataItem, "FromServiceUnitName")%></b>
                            <br />
                            By:&nbsp;<i> <%#DataBinder.Eval(Container.DataItem, "PhysicianSenders")%></i>
                            <br />
                            Order Item:
                            <br />
                            <div style="padding-left: 10px;">
                                <%#DataBinder.Eval(Container.DataItem, "JobOrderSummary")%>
                            </div>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="LaboratoryResult" HeaderText="Result">
                        <ItemTemplate>
                            <%#Temiang.Avicenna.Module.RADT.Emr.MainContent.ExamOrderHistCtl.LaboratoryResultNote(DataBinder.Eval(Container.DataItem, "TransactionNo").ToString())%>
                            <telerik:RadGrid ID="grdLaboratoryResult" runat="server" AutoGenerateColumns="False" GridLines="None">
                                <MasterTableView DataKeyNames="OrderLabNo" GroupLoadMode="Client">
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="TestGroup" HeaderText="Group" />
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="TestGroup" SortOrder="None" />
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridTemplateColumn DataField="LabOrderSummary" UniqueName="LabOrderSummary"
                                            HeaderText="Exam Name" HeaderStyle-Width="250px">
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "LabOrderSummary")%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridDateTimeColumn DataField="ResultDatetime" UniqueName="ResultDatetime" HeaderText="Result Date" HeaderStyle-Width="120px" />
                                        <telerik:GridBoundColumn DataField="Flag" UniqueName="Flag" HeaderText="Flag" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Result" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Result")%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn DataField="StandarValue" UniqueName="StandarValue" HeaderText="Standard Value"
                                            HeaderStyle-Width="150px" />
                                        <telerik:GridBoundColumn DataField="ResultComment" UniqueName="ResultComment" HeaderText="Result Comment" />
                                        <telerik:GridBoundColumn DataField="LabOrderCode" UniqueName="LabOrderCode" HeaderText="Code" Display="False" />
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="False">
                                    <Selecting AllowRowSelect="False" />
                                    <Scrolling UseStaticHeaders="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="False">
                <Selecting AllowRowSelect="False" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            </ClientSettings>
        </telerik:RadGrid>

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
