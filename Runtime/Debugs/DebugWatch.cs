using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

// 2021. 02. 19. 15:00
// 작성자 : Rito

/*
   [ 기록]
   2023. 03. 26.
   - Ut23 라이브러리에 편입
   - EndCheck() 메소드 리턴값 추가: 해당 측정 인덱스
*/

namespace Rito.ut23.Debugs
{
    public static class DebugWatch
    {
        private class Record
        {
            public string content;
            public long elapsedMS;
            public long elapsedTicks;
        }

        private static Stopwatch _sw;
        private static int _index;
        private static Dictionary<int, Record> _dictRecord;
        private static bool _isChecking;

        static DebugWatch()
        {
            _sw = new Stopwatch();
            _dictRecord = new Dictionary<int, Record>();
            _index = 0;
        }

        public static void Clear()
        {
            _dictRecord.Clear();
            _index = 0;
        }

        public static void BeginCheck(string content)
        {
            if (_isChecking) EndCheck();

            _dictRecord.Add(_index, new Record());
            _dictRecord[_index].content = content;

            _sw.Restart();
            _isChecking = true;
        }

        public static int EndCheck()
        {
            int ret = _index;
            _sw.Stop();
            _dictRecord[_index].elapsedMS = _sw.ElapsedMilliseconds;
            _dictRecord[_index].elapsedTicks = _sw.ElapsedTicks;

            _index++;
            _isChecking = false;
            return ret;
        }

        public static void PrintLog(int index)
        {
            if(_isChecking) throw new StowatchStillRunningException();

            var record = _dictRecord[index];
            string msg = $"[{index}] {record.content}\n ms : {record.elapsedMS}, ticks : {record.elapsedTicks}";
            UnityEngine.Debug.Log(msg);
        }

        public static void PrintAllLogs()
        {
            for (int i = 0; i < _index; i++)
            {
                PrintLog(i);
            }
        }

        /***********************************************************************
        *                               Exception
        ***********************************************************************/
        #region .
        public class StopwatchAlreadyRunningException : System.Exception
        {
            public StopwatchAlreadyRunningException() : base("Stopwatch Already Running") { }
        }

        public class StowatchStillRunningException : System.Exception
        {
            public StowatchStillRunningException() : base("Please Stop") { }
        }

        #endregion
    }
}