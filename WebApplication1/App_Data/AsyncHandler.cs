using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebApplication1
{
    public class AsyncHandler : IHttpAsyncHandler
    {
        public bool IsReusable { get { return false; } }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            context.Response.Write("<p>Begin isThreadPoolThread is" +Thread.CurrentThread.IsThreadPoolThread + "</p>\r\n");
            var async = new AsynchOperation(cb, context, extraData);
            async.StartAsyncWork();
            return async;
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            
        }
        public void ProcessRequest(HttpContext context)
        {
            throw new InvalidOperationException();
        }
    }

    public class AsynchOperation : IAsyncResult
    {
        private AsyncCallback _callback;
        private object state;
        private HttpContext _context;
        public bool Compleated { get; set; }

        public AsynchOperation(AsyncCallback cb, HttpContext context, object state)
        {
            
            this._callback = cb;
            this._context = context;
            this.state = state;
            Compleated = false;
        }

        public object AsyncState{get { return state; }}

        public WaitHandle AsyncWaitHandle{get { return null; }}

        public bool CompletedSynchronously{get { return false; }}

        public bool IsCompleted{get { return Compleated; }}

        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }

        private void StartAsyncTask(object state)
        {
            _context.Response.Write("<p>Completion isThreadPool</p>");
            _context.Response.Write("Hello from async handler");
            Compleated = true;
            _callback(this);
        }
    }
}