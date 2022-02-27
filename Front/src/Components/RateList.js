import react, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import moment from 'moment';
import NewsDetails from '../Page/NewsDetails';


export default function RateList() {
    const [RateListAPI, setRateListAPI] = useState([]);
    const [Loading, setLoading] = useState(true)
    useEffect(() => {
        axios.get("https://localhost:44352/api/ratelist/").then((response) => {
            setLoading(true);
            setRateListAPI(response.data);
            setLoading(false);
        }).catch((err) => err.name);
    }, [])

    return (
        <div>
            {
                Loading === true ?
                    <div className="row align-content-center">
                        <div className="d-flex justify-content-center p-5">
                            <div className="spinner-border text-info spinner-size" role="status">
                                <span className="visually-hidden">Loading...</span>
                            </div>
                        </div>
                        <div className="d-flex justify-content-center p-5">
                            Please wait
                        </div>
                    </div> :
                    RateListAPI.map((data) => {
                        return (
                            <div key={data.id} className="">
                                <div className="list-group">
                                    <div href="#" class="list-group-item list-group-item-action pb-3 mb-3" aria-current="true">
                                        <div className="d-flex w-100 justify-content-between ">
                                            <h5 className="mb-2 pb-4">{data.name}</h5>
                                            {data.rate>7 ? <p className="btn btn-success rounded-circle">{data.rate}</p>:
                                                data.rate<=7 && data.rate>5 ? <p className="btn btn-warning rounded-circle">{data.rate}</p>
                                                :
                                                <p className="btn btn-danger rounded-circle">{data.rate}</p>
                                            }
                                        </div>
                                        <small>Platforms: {data.platforms}</small>
                                    </div>
                                </div>
                            </div>

                        )
                    })
            }

        </div>
    )
}



