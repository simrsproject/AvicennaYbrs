<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="FilmConsumptionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.FilmConsumptionDetail" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinProcess(joNo, itemID, seqNo, unit, type) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.setUrl("FilmConsumptionTariffComponent.aspx?joNo=" + joNo + "&itemID=" + itemID + "&seqNo=" + seqNo + "&unitID=" + unit + "&type=" + type + '&disch=<%= Page.Request.QueryString["disch"] %>');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinFilm(joNo, itemID, seqNo) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.setUrl("../JobOrderRealisation/ExposureFactorDialog.aspx?joNo=" + joNo + "&itemID=" + itemID + "&seqNo=" + seqNo);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.command == 'rebind') {
                    __doPostBack("<%= grdTransChargesItem.UniqueID %>", 'rebind');
                }
            }

            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdTransChargesItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="500px"
        Behavior="Close, Move" ShowContentDuringLoad="False" VisibleStatusbar="false"
        Modal="true" ID="winProcess" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image1" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Registration Detail">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
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
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
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
                                                    <td style="width: 3px"></td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="266px" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtSex" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="30px" ReadOnly="true">
                                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                            &nbsp;Y&nbsp;
                                            <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="30px" ReadOnly="true">
                                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                            &nbsp;M&nbsp;
                                            <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="30px" ReadOnly="true">
                                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                            &nbsp;D&nbsp;
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRadiologyNo" runat="server" Text="Radiology No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtRadiologyNo" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblPhysicianSenders" runat="server" Text="Physician Senders"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPhysicianSenders" runat="server" Width="300px" MaxLength="255" />
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvPhysicianSenders" runat="server" ErrorMessage="Physician Senders required."
                                                ValidationGroup="entry" ControlToValidate="txtPhysicianSenders" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblServiceUnitName" runat="server"></asp:Label>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblRoomName" runat="server"></asp:Label>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblClassID" runat="server" Text="Charge Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblClassName" runat="server"></asp:Label>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblParamedicName" runat="server"></asp:Label>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblGuarantorName" runat="server"></asp:Label>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <fieldset id="FieldSet1" style="width: 150px; min-height: 150px;">
                                    <legend>Photo</legend>
                                    <asp:Image runat="server" ID="imgPatientPhoto" Width="150px" Height="150px" />
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td width="25%" class="labelcaption">
                                <asp:Label runat="server" ID="lblChiefComplaint" Text="Chief Complaint" />
                            </td>
                            <td width="25%" class="labelcaption">
                                <asp:Label runat="server" ID="Label1" Text="Anamnesis" />
                            </td>
                            <td width="25%" class="labelcaption">
                                <asp:Label runat="server" ID="Label2" Text="Initial Diagnose" />
                            </td>
                            <td width="25%" class="labelcaption">
                                <asp:Label runat="server" ID="Label3" Text="Medication Planning" />
                            </td>
                        </tr>
                        <tr>
                            <td width="25%">
                                <telerik:RadTextBox ID="txtChiefComplaint" runat="server" Width="98%" ReadOnly="true"
                                    Height="73px" TextMode="MultiLine" />
                            </td>
                            <td width="25%">
                                <telerik:RadTextBox ID="txtAnamnesis" runat="server" Width="98%" ReadOnly="true"
                                    Height="73px" TextMode="MultiLine" />
                            </td>
                            <td width="25%">
                                <telerik:RadTextBox ID="txtInitialDiagnose" runat="server" Width="98%" ReadOnly="true"
                                    Height="73px" TextMode="MultiLine" />
                            </td>
                            <td width="25%">
                                <telerik:RadTextBox ID="txtPlanning" runat="server" Width="98%" ReadOnly="true" Height="73px"
                                    TextMode="MultiLine" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
        PageSize="15" OnItemCreated="grdTransChargesItem_ItemCreated">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="PhysicianTemplateColumn2" SortExpression="RoomName"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="50px" />
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "TransactionCorrectionNo").Equals(string.Empty) || DataBinder.Eval(Container.DataItem, "TransactionCorrectionNo").Equals(null) ? string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}', '{1}', '{2}', '{3}', '{4}'); return false;\"><img src=\"../../../../Images/Toolbar/dokter.png\" border=\"0\" title=\"Edit Physician\" /></a>",
                                                                                        DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "ItemID"),
                                                                                                                                                                        DataBinder.Eval(Container.DataItem, "SequenceNo"), DataBinder.Eval(Container.DataItem, "ToServiceUnitID"), DataBinder.Eval(Container.DataItem, "SRItemType")) : string.Empty%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="ExposureFactor" SortExpression="ExposureFactor"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="50px" />
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openWinFilm('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/radfilm.png\" border=\"0\" title=\"Exposure Factor\" /></a>",
                                                                                        DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "ItemID"),
                                                                                                                                                                        DataBinder.Eval(Container.DataItem, "SequenceNo"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                    Visible="false" />
                <telerik:GridTemplateColumn UniqueName="ItemName" HeaderText="Item Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                        <br />
                        Notes&nbsp;:&nbsp;<%# DataBinder.Eval(Container.DataItem, "Notes")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ParamedicCollectionName" HeaderText="Physician">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ParamedicCollectionName")%>
                        <br />
                        <i>Film No</i>&nbsp;:&nbsp;<%# DataBinder.Eval(Container.DataItem, "FilmNo")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ChargeQuantity" HeaderText="Qty"
                    UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CitoAmount" HeaderText="Cito"
                    UniqueName="CitoAmount" SortExpression="CitoAmount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="ChargeQuantity,Price,DiscountAmount,CitoAmount"
                    SortExpression="Total" Expression="{0} * (({1} - {2}) + {3})" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsCito" HeaderText="Cito"
                    UniqueName="IsCito" SortExpression="IsCito" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
