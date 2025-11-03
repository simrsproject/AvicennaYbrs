<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" 
CodeBehind="PaymentInfoDialog.aspx.cs" 
Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.V2.PaymentInfoDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script language="javascript" type="text/javascript">
            function rowRecalFeePercByAjax(keyId, invoiceNo) {
                if (confirm('Are you sure want to recalculate payment percentage?')) {
                    $.ajax({
                        type: 'POST',
                        url: "<%=Page.ResolveUrl("~/Home/ParSetProporsiJasmed")%>",
                        data: { InvoiceNo: invoiceNo },
                        success: function (data) {
                            //console.log(data);
                            if (data.status === 'OK') {
                                //console.log(data.data);

                            } else {
                                //DisplayToast(ret.data, "error");
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            //DisplayToast(xhr.responseText, "error");
                        },
                        dataType: 'json'
                    });

                    var divCtl = $(document.getElementById(keyId));
                    //console.log(divCtl.attr("group"));
                    var par = divCtl.attr("group");

                    var divCtl = $(document.getElementById(invoiceNo));
                    DisplayStatus(keyId, divCtl, '01', 0, '', par, 1);
                }

                return false;
            }

            function DisplayStatus(keyId, jQueryDiv, status, progress, errMsg, invoiceNo, iCounter) {
                var txtStatus = "";
                switch (status) {
                    case "01":
                    case "02": {
                        txtStatus = "<img src=\"<%=Page.ResolveUrl("~\\Images\\Toolbar\\cancel16.png")%>\" class=\"icoSpin\" style=\"" + ((status == "01") ? "filter: hue-rotate(120deg)" : "filter: saturate(10)") +"\"> " + progress + "%";
                        //console.log(txtStatus);
                        var iDelay = (iCounter == 0) ? 0 : 3000;
                        // ajax update here
                        setTimeout(function () {
                            iCounter = iCounter + 1;
                            $.ajax({
                                type: 'POST',
                                url: "<%=Page.ResolveUrl("~/Home/ParGetProgressProporsiJasmed")%>",
                                data: { InvoiceNo: invoiceNo },
                                success: function (data) {
                                    //console.log(data);
                                    if (data.status === 'OK') {
                                        //console.log(data.data);

                                        DisplayStatus(
                                            keyId,
                                            jQueryDiv,
                                            data.data.SRPhysicianFeeProportionalStatus,
                                            data.data.PhysicianFeeProportionalPercentage,
                                            data.data.PhysicianFeeProportionalErrMessage,
                                            invoiceNo, iCounter);
                                    } else {
                                        txtStatus = "";
                                        jQueryDiv.html(txtStatus);
                                    }
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    //DisplayToast(xhr.responseText, "error");
                                },
                                dataType: 'json'
                            });
                        }, iDelay);
                        break;
                    } case "04": {
                        txtStatus = "<a href=\"javascript: void(0);\" onclick=\"return rowRecalFeePercByAjax('" + keyId + "','" + invoiceNo + "');\"><img src=\"<%=Page.ResolveUrl("~\\Images\\Toolbar\\refresh16.png")%>\" class=\"grayscale\" title=\"Error: " + errMsg + "\" ></a>";
                        break;
                    } default: {
                        txtStatus = "<a href=\"javascript: void(0);\" onclick=\"return rowRecalFeePercByAjax('" + keyId + "','" + invoiceNo + "');\"><img src=\"<%=Page.ResolveUrl("~\\Images\\Toolbar\\refresh16.png")%>\" ></a>";
                        break;
                    }
                }
                jQueryDiv.html(txtStatus);
            }

            function RowCreated(sender, eventArgs) {
               <%-- var IsPropor = '<%= IsFeeCalculateProporsionalOnPayment() %>' == 'True';

                if (!IsPropor) return;--%>

                var keyId = eventArgs.get_gridDataItem().get_element().cells[1].textContent;
                keyId += "_" + eventArgs.get_gridDataItem().get_element().cells[2].textContent;
                console.log(keyId);

                var parStatus = '01';//eventArgs.get_gridDataItem().get_element().cells[8].textContent;
                var parProgress = 0;//eventArgs.get_gridDataItem().get_element().cells[9].textContent;

                var divCtl = $(document.getElementById(keyId));
                //console.log(divCtl.attr("group"));
                var par = divCtl.attr("group");

                //$("[id=choose]")

                //console.log(ParStatus);

                DisplayStatus(keyId, divCtl, parStatus, parProgress, '', par, 0);
            }

            function onDataBinding(e) {
                e.preventDefault();
            }
        </script>
    </telerik:RadCodeBlock>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <telerik:RadGrid ID="grdPayment" runat="server" OnNeedDataSource="grdPayment_NeedDataSource" OnItemCommand="grdPayment_ItemCommand"
        AllowPaging="False" AllowSorting="true" ShowStatusBar="true">
        <MasterTableView DataKeyNames="PaymentNo" ClientDataKeyNames="PaymentNo" AutoGenerateColumns="false">
            <Columns>
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor Name" UniqueName="GuarantorName"
                    SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PaymentNo" HeaderText="Payment No" UniqueName="PaymentNo"
                    SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="0px" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="" UniqueName="SequenceNo"
                    SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="0px" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridTemplateColumn UniqueName="PaymentNo" HeaderText="Payment No" Groupable="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PaymentNo")%>
                        <asp:LinkButton ID="lbPaymentNo" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "PaymentNo")%>'
                            CommandName="recalFeeByPM" ToolTip="Reprocessing Payment"
                            OnClientClick="return confirm('Are you sure want to reprocessing payment?');" Visible='<%#!string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "PaymentNo").ToString())%>'>
                            <img style="border: 0px; text-align:center; vertical-align: middle;" src="../../../../Images/Toolbar/refresh16.png" />
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="PaymentMethodName" HeaderText="Payment Method" UniqueName="PaymentMethodName"
                    SortExpression="PaymentMethodName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ApproveDate" HeaderText="Approve Date" UniqueName="ApproveDate"
                    SortExpression="ApproveDate" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Amount" HeaderText="Amount" UniqueName="Amount"
                    SortExpression="Amount" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="InvoiceNo" HeaderText="Invoice No" UniqueName="InvoiceNo"
                    SortExpression="InvoiceNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="InvoiceApproveDate" HeaderText="Invoice Approve Date" UniqueName="InvoiceApproveDate"
                    SortExpression="InvoiceApproveDate" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="InvoiceAmount" HeaderText="Invoice Amount" UniqueName="InvoiceAmount"
                    SortExpression="InvoiceAmount" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="InvoicePaymentNo" HeaderText="" UniqueName="InvoicePaymentNo"
                    SortExpression="InvoicePaymentNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="0px" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn UniqueName="RecalFeePerc" HeaderText="Invoice Payment" Groupable="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "InvoicePaymentNo")%>
                        <%# string.Format("<div id=\"{0}_{1}\" group=\"{2}\" />", DataBinder.Eval(Container.DataItem, "PaymentNo").ToString(),DataBinder.Eval(Container.DataItem, "SequenceNo").ToString(), DataBinder.Eval(Container.DataItem, "InvoicePaymentNo").ToString()) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="InvoicePaymentApproveDate" HeaderText="Invoice Payment Approve Date" UniqueName="InvoicePaymentApproveDate"
                    SortExpression="InvoicePaymentApproveDate" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="InvoicePaymentAmount" HeaderText="Invoice Payment Amount" UniqueName="InvoicePaymentAmount"
                    SortExpression="InvoicePaymentAmount" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <ClientEvents OnRowCreated="RowCreated" />
        </ClientSettings>
    </telerik:RadGrid>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            </td>
            <td class="entry">

            </td>
            <td width="20px">
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>