import react, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { BrowserRouter, Route } from 'react-router-dom';
import LastNewsHome from './LastNewsHome';
import { getDefaultNormalizer } from '@testing-library/dom';

export default function HomeCategories() {

    const [CategoryApi, setCategoryApi] = useState([]);
    const [CategoryId, setCategoryId] = useState();
    const [NewsApi, setNewsApi] = useState([]);
    const [Loading, setLoading] = useState(true);
    useEffect(() => {
        axios.get("https://localhost:44352/api/category/").then((response) => {
            setCategoryApi(response.data);
        })
        setLoading(false);
    }, [])


    const getData = (data) => {
        setLoading(true)
        setTimeout(function () {
            setCategoryId(data.id);
            setLoading(false)
        }, 1000);


    }
    return (
        <div>
            <div>
                {CategoryApi.map((data) => {
                    return (
                        <button key={data.id} type="submit" onClick={() => getData(data)} className="btn btn-sm btn-outline-light text-dark rounded-pill shadow m-2" >{data.categoryName}</button>
                    )
                })
                }
            </div>
            {
                Loading == true ?
                    <div className="row align-content-center">
                        <div className="d-flex justify-content-center p-5">
                            <div className="spinner-border text-info spinner-size" role="status">
                                <span className="visually-hidden">Loading...</span>
                            </div>
                        </div>
                        <div className="d-flex justify-content-center p-5">
                            Please wait
                        </div>
                    </div>
                    : <div className="pt-3 text-start">
                        <h2 className="text-danger fw-bolder pb-4">Last News</h2>
                        <LastNewsHome categoryId={CategoryId} setLoading={setLoading} />
                    </div>
            }

        </div>
    )
}





