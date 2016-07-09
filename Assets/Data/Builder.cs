using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Data {
    public class Builder{
        private string _playerId;
        private string _uniqId;

        private bool _isMoveThisRound;
        private bool _isBuildThisRound;

        public bool IsBuildThisRound{
            get { return _isBuildThisRound; }
            set { _isBuildThisRound = value; }
        }

        public bool IsMoveThisRound{
            get { return _isMoveThisRound; }
            set { _isMoveThisRound = value; }
        }

        public string PlayerId{
            get { return _playerId; }
        }

        public string UniqId{
            get { return _uniqId; }
        }

        public Builder(string PlayerId, string UniqId){
            _playerId = PlayerId;
            _uniqId = UniqId;
            _isMoveThisRound = false;
        }

        
    }
}
