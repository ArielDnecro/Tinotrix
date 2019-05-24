<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AreaControl.ascx.cs" Inherits="CodorniX.Template.AreaControl" %>
<asp:PlaceHolder ID="mainContainer" runat="server">
    <div class="row" style="padding-top: 10px;">
        <asp:TextBox ID="uid" CssClass="disabled hidden" runat="server" />
        <div class="col-md-10">
            <asp:TextBox ID="textBox" CssClass="form-control" runat="server" placeholder="Nombre del área" />
        </div>
        <div class="col-md-2">
            <asp:LinkButton ID="add" CssClass="btn btn-success btn-sm" OnClick="add_Click" runat="server">
                <span class="glyphicon glyphicon-plus"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="remove" CssClass="btn btn-default btn-sm disabled hidden" OnClick="remove_Click" runat="server">
                <span class="glyphicon glyphicon-trash"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="edit" CssClass="btn btn-default btn-sm disabled hidden" OnClick="edit_Click" runat="server">
                <span class="glyphicon glyphicon-edit"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="ok" CssClass="btn btn-success btn-sm disabled hidden" OnClick="ok_Click" runat="server">
                <span class="glyphicon glyphicon-ok"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="cancel" CssClass="btn btn-danger btn-sm disabled hidden" OnClick="cancel_Click" runat="server">
                <span class="glyphicon glyphicon-remove"></span>
            </asp:LinkButton>
        </div>
    </div>
</asp:PlaceHolder>
