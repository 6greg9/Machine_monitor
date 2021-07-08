using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public interface IChatClient
    {
        /// <summary>
        /// SignalR接收信息
        /// </summary>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        Task ReceiveMessage(object message);

        /// <summary>
        /// SignalR接收信息
        /// </summary>
        /// <param name="user">指定接收客户端</param>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        Task ReceiveMessage(string user, string message);

        Task ReceiveUpdate(object message);

        Task ReceiveMachineData(string machineId, string message);
        Task ReceiveMachine(string machineList);
        Task GetRequestMachineData(string userId);
    }

    public class MonitorHub : Hub<IChatClient>
    {

        /// <summary>
        /// 向指定群组发送信息
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        public async Task SendMessageToGroupAsync(string groupName, string message)
        {
            await Clients.Group(groupName).ReceiveMessage(message);
        }

        /// <summary>
        /// 加入指定组
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 退出指定组
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 向指定成员发送信息
        /// </summary>
        /// <param name="user">成员名</param>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        public async Task SendPrivateMessage(string user, string message)
        {
            await Clients.User(user).ReceiveMessage(message);
        }

        /// <summary>
        /// 当连接建立时运行
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            //TODO..
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// 当链接断开时运行
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(System.Exception ex)
        {
            //TODO..
            return base.OnDisconnectedAsync(ex);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        
        public async Task WinFormsApp(string name, string msg)
        {
            await Clients.All.ReceiveUpdate($"接收到{name}的消息,消息内容：{msg}");
        }
        public async Task SendMachine(string sendMachineData)
        {
            await Clients.All.ReceiveMachine(sendMachineData);
        }
        public async Task SendMachineData(string machineId, string sendMachineData)
        {
            await Clients.All.ReceiveMachineData(machineId,sendMachineData);
        }
        public async Task AddToDataSource()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "DataSource");
        }
        
        public async Task RequestMachineData()
        {
            await Clients.Group("DataSource").GetRequestMachineData(Context.ConnectionId);
        }
        public async Task SendUserMachine(string user, string sendMachine)
        {
            await Clients.Client(user).ReceiveMachine(sendMachine);
        }

    }


}