import React from 'react';

const style = {
    display: 'grid',
    gridTemplateColumns: '1fr 1fr',
    gridGap: 16,
    
  };

const Flag = () => {
    return(
        <div style={style}>
            <div>English</div>
            <div>Netherlands</div>
        </div>
    );
}

export default Flag;