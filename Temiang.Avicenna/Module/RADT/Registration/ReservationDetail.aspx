<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="ReservationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.ReservationDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReservationNo" runat="server" Text="Reservation No"></asp:Label>
			            </td>
			            <td class="entry2Column">
				            <telerik:RadTextBox ID="txtReservationNo" runat="server" Width="300px" MaxLength="20"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReservationNo" runat="server" ErrorMessage="Reservation No required."
                    	            ValidationGroup="entry" ControlToValidate="txtReservationNo" 
						            SetFocusOnError="True" Width="100%">
							            <asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPatientID_ItemDataBound"
                                OnItemsRequested="cboPatientID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                    </b>&nbsp;-&nbsp;
                                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                    &nbsp;|&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "Address")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReservationDate" runat="server" Text="Reservation Date"></asp:Label>
			            </td>
			            <td class="entry">
				            <telerik:RadDatePicker ID="txtReservationDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReservationDate" runat="server" ErrorMessage="Reservation Date required."
                    	            ValidationGroup="entry" ControlToValidate="txtReservationDate" 
						            SetFocusOnError="True" Width="100%">
							            <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReservationTime" runat="server" Text="Reservation Time"></asp:Label>
			            </td>
			            <td class="entry">
				            <telerik:RadTextBox ID="txtReservationTime" runat="server" Width="80px" MaxLength="5"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReservationTime" runat="server" ErrorMessage="Reservation Time required."
                    	            ValidationGroup="entry" ControlToValidate="txtReservationTime" 
						            SetFocusOnError="True" Width="100%">
							            <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trSRAppointmentStatus" visible="false">
                        <td class="label">
                            <asp:Label ID="lblSRAppointmentStatus" runat="server" Text="ReservationStatus Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRReservationStatus" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true" AutoPostBack="true"
                                OnItemDataBound="cboServiceUnitID_ItemDataBound" OnItemsRequested="cboServiceUnitID_ItemsRequested">
                                <ItemTemplate>
                                   <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="Registration" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image21" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room ID"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadComboBox ID="cboRoomID" runat="server" Width="300px" AutoPostBack="true"
                                HighlightTemplatedItems="True" MarkFirstMatch="true" OnItemDataBound="cboRoomID_ItemDataBound" 
                                OnItemsRequested="cboRoomID_ItemsRequested" EnableLoadOnDemand="true" NoWrap="True">
                                <ItemTemplate>
                                   
                                    <%# DataBinder.Eval(Container.DataItem, "RoomName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRoomID" runat="server" ErrorMessage="Room required."
                                ValidationGroup="Registration" ControlToValidate="cboRoomID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image22" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>        
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBedID" runat="server" Text="Bed ID"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadComboBox ID="cboBedID" runat="server" Width="300px" AutoPostBack="true"
                                HighlightTemplatedItems="True"  OnItemDataBound="cboBedID_ItemDataBound" 
                                OnItemsRequested="cboBedID_ItemsRequested" OnSelectedIndexChanged="cboBedID_SelectedIndexChanged"
                                MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvBedID" runat="server" ErrorMessage="Bed required."
                                ValidationGroup="Registration" ControlToValidate="cboBedID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image24" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClassID" runat="server" Text="Class ID"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100px">
                                        <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" MaxLength="10" ReadOnly="true" />
                                    </td>
                                    <td style="width: 6px">
                                    </td>
                                    <td>
                                        <asp:Label ID="lblClassName_NT" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvClassID" runat="server" ErrorMessage="Class ID required."
                                ValidationGroup="Registration" ControlToValidate="txtClassID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>        
	                <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
			            </td>
			            <td class="entry">
				            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="2000" Height="80px" />
                        </td>
                        <td width="20px">
                            
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>    