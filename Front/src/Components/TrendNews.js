import react, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { BrowserRouter, Route } from 'react-router-dom';

export default function TrendNews() {
    const BaseImageUrl = "https://localhost:44352/News/";
    const [TrendNewsApi, setTrendNewsApi] = useState([]);
    const [Loading, setLoading] = useState(true)
    useEffect(() => {
        axios.get("https://localhost:44352/api/news/GetNewsByTrend/").then((response) => {
            getData(response.data)
        }).catch((err) => err.name);

    }, [])
    const getData = (data) => {
        setLoading(true)
        setTrendNewsApi(data)
        setTimeout(function () {
            setLoading(false)
        }, 1000);
    }

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
                    <div className="row">
                        {
                            TrendNewsApi.map((data) => {
                                return (

                                    <div className="col-md-3 h-100" key={data.id}>
                                        <Link to={"/NewsDetails/" + data.id} className="text-decoration-none link-dark">
                                            <div className="mb-4">
                                                <div className="pt-2 pb-2">
                                                    <img src={BaseImageUrl + data.imageOne} className="card-img-left rounded-3 img-fluid shadow-lg" />
                                                </div>
                                                <h4 className="pt-2 pb-2">
                                                    {data.title}
                                                </h4>
                                                <button className={data.id % 2 === 0 ? "btn badge rounded-pill shadow bg-info" : "btn badge rounded-pill shadow bg-warning"}>{data.categories.categoryName}</button>
                                            </div>
                                        </Link>
                                    </div>


                                )
                            })
                        }
                    </div>
            }
        </div>
    )
}